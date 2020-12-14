using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System;
using System.Linq;
using System.Threading;
using System.Text;

namespace Networking
{
    public class NetworkManager : MonoBehaviour
    {
        #region [Singletone]
        private static NetworkManager _instance;
        public static NetworkManager Instance 
        {
            get
            {
                if (_instance == null)
                {
                    var instance = FindObjectOfType<NetworkManager>();
                    if (instance != null)
                    {
                        Instance = instance;
                    }
                    else
                    {
                        Debug.LogError("No instance found");
                    }
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }
        private void Awake()
        {
            if (Instance != this)
            {
                Debug.LogError("More than one instance");
                Destroy(gameObject);
            }
        }
        #endregion
        [SerializeField] private string host = "127.0.0.1";
        [SerializeField] private int port = 8888;
        private TcpClient _client;
        private Thread clientReceiveThread;
        private int _id;
        private bool _connected = false;
        private NetworkStream _stream;
        public static int Id { get => Instance._id; set => Instance._id = value; }
        private void Start()
        {
            Connection();
        }
        private void Connection()
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(host, port);
                _connected = true;
                _stream = _client.GetStream();
                
                clientReceiveThread = new Thread(ListenForData);
                clientReceiveThread.IsBackground = true;
                clientReceiveThread.Start();

            }
            catch (Exception e)
            {
                _connected = false;
                Debug.Log("On client connect exception " + e);
            }
        }

        private void ListenForData()
        {
            try
            {
                while (true)
                {		
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    var builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = _stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (_stream.DataAvailable);
 
                    string message = builder.ToString();
                    Debug.Log(message);//вывод сообщения
                }
            }
            catch (SocketException socketException)
            {
                Disconnect();
                Debug.LogError("Socket exception: " + socketException);
            }
        }
        
        public void SendMessageToServer(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            _stream.Write(data, 0, data.Length);
        }

        private void OnDestroy()
        {
            Debug.Log("destroy?");
            Disconnect();
        }
        private void Disconnect()
        {
            _connected = false;
            _stream.Close();
            _client?.Close();
            clientReceiveThread.Abort();
        }

        private void OnDisable()
        {
            Disconnect();
        }
        
    }
}

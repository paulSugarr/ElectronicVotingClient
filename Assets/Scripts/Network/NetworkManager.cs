using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System;
using System.Linq;
using System.Threading;
using System.Text;
using Loggers;

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

        public bool Connected => _connected;

        private string _host = "127.0.0.1";
        private int _port = 8888;
        private TcpClient _client;
        private Thread _clientReceiveThread;
        private bool _connected = false;
        private NetworkStream _stream;

        public void SetupManager(string host, int port)
        {
            _host = host;
            _port = port;
        }
        private void Start()
        {
            this.Print("d");
            Connect();
        }
        public void Connect()
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(_host, _port);
                _connected = true;
                _stream = _client.GetStream();
                
                _clientReceiveThread = new Thread(ListenForData);
                _clientReceiveThread.IsBackground = true;
                _clientReceiveThread.Start();

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
                    byte[] data = new byte[64];
                    var builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = _stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (_stream.DataAvailable);
 
                    string message = builder.ToString();
                    Debug.Log(message);
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
            Disconnect();
        }
        private void Disconnect()
        {
            _connected = false;
            _client?.Close();
            _clientReceiveThread.Abort();
        }

    }
}

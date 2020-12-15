using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Core;
using Extensions;
using Networking.Commands;
using UnityEngine;

namespace Networking
{
    public class TCPServer
    {
        public bool Connected => _connected;
        
        private readonly string _host;
        private readonly int _port;
        private TcpClient _client;
        private Thread _clientReceiveThread;
        private bool _connected = false;
        private NetworkStream _stream;


        public TCPServer(string host, int port)
        {
            _host = host;
            _port = port;
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
                    byte[] data = new byte[1024];
                    var builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = _stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (_stream.DataAvailable);
 
                    string message = builder.ToString();
                    if (message.Length <= 2)
                    {
                        throw new Exception("Short message");
                    }
                    
                    Debug.Log($"msg = {message}");
                    var jsonData = fastJSON.JSON.Parse(message).ToDictionary();
                    Debug.Log($"Incoming command type = {jsonData["key"]}");
                    var command = Context.Instance.MainFactory.CreateInstance<ICommand>(jsonData);
                    command.Execute();
                    
                    // try
                    // {
                    //     var jsonData = fastJSON.JSON.Parse(message).ToDictionary();
                    //     Debug.Log($"Incoming command type = {jsonData["key"]}");
                    //     var command = Context.Instance.MainFactory.CreateInstance<ICommand>(jsonData);
                    //     command.Execute();
                    // }
                    // catch (Exception e)
                    // {
                    //     Debug.LogError($"Incorrect incoming command. Error = {e}");
                    // }

                }
            }
            catch (SocketException socketException)
            {
                Disconnect();
                Debug.LogError("Socket exception: " + socketException);
            }
        }
        
        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            _stream.Write(data, 0, data.Length);
        }


        public void Disconnect()
        {
            _connected = false;
            _client?.Close();
            _clientReceiveThread.Abort();
        }
    }
}
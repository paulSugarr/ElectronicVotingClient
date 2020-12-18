using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System;
using System.Linq;
using System.Threading;
using System.Text;
using ElectronicVoting.Extensions;
using Loggers;
using Networking.Commands;
using UI;

namespace Networking
{
    public class NetworkManager
    {
        private readonly TCPServer _validator;
        private readonly TCPServer _agency;

        private Thread _connectionThread;

        public NetworkManager(Dictionary<string, object> mainConfig)
        {
            var validatorHost = mainConfig.GetString("validator_ip");
            var validatorPort = mainConfig.GetInt("validator_port");
            _validator = new TCPServer(validatorHost, validatorPort);
            
            var agencyHost = mainConfig.GetString("agency_ip");
            var agencyPort = mainConfig.GetInt("agency_port");
            _agency = new TCPServer(agencyHost, agencyPort);
        }
        public void Activate()
        {
            _validator.Connected += OnValidatorConnect;
        }

        public void Disconnect()
        {
            _validator.Disconnect();
            _agency.Disconnect();
        }

        public void Connect()
        {
            _agency.Connect();
            _validator.Connect();
        }

        public void StopConnecting()
        {
            _connectionThread.Join();
        }

        public bool IsConnected()
        {
            return _agency.IsConnected && _validator.IsConnected;
        }

        public void SendMessageToValidator(string message)
        {
            _validator.SendMessage(message);
        }
        public void SendCommandToValidator(ICommand command)
        {
            var commandInfo = command.GetInfo();
            var message = fastJSON.JSON.ToJSON(commandInfo);
            SendMessageToValidator(message);
        }
        
        public void SendMessageToAgency(string message)
        {
            _agency.SendMessage(message);
        }
        public void SendCommandToAgency(ICommand command)
        {
            var commandInfo = command.GetInfo();
            var message = fastJSON.JSON.ToJSON(commandInfo);
            SendMessageToAgency(message);
        }

        private void OnValidatorConnect()
        {
            var command = new SendValidatorKeyCommand();
            SendCommandToValidator(command);
        }
        
    }
}

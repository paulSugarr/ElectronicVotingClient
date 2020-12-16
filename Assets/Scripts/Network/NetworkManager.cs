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

namespace Networking
{
    public class NetworkManager
    {
        private readonly TCPServer _validator;
        private readonly TCPServer _agency;

        public NetworkManager(Dictionary<string, object> mainConfig)
        {
            var validatorHost = mainConfig.GetString("validator_ip");
            var validatorPort = mainConfig.GetInt("validator_port");
            _validator = new TCPServer(validatorHost, validatorPort);
            
            var agencyHost = mainConfig.GetString("agency_ip");
            var agencyPort = mainConfig.GetInt("agency_port");
            _agency = new TCPServer(agencyHost, agencyPort);
        }

        public void Disconnect()
        {
            _validator.Disconnect();
            _agency.Disconnect();
        }

        public void Connect()
        {
            _validator.Connect();
            _agency.Connect();
        }

        public bool IsConnected()
        {
            return _agency.Connected && _validator.Connected;
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
    }
}

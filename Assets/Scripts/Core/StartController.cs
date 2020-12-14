using System;
using System.Collections.Generic;
using System.IO;
using Extensions;
using UnityEngine;
using fastJSON;
using Networking;

namespace Core
{
    public class StartController : MonoBehaviour, IController
    {
        [SerializeField] private StartReferences _startReferences;

        private Dictionary<string, object> _mainConfig;
        private void OnDestroy()
        {
            Deactivate();
        }

        private void Start()
        {
            Activate();
        }
        
        public void Deactivate()
        {
            throw new System.NotImplementedException();
        }

        public void Activate()
        {
            _mainConfig = ReadMainConfig(_startReferences.MainConfigPath);
            SetupNetworkManager(_mainConfig);
        }

        private Dictionary<string, object> ReadMainConfig(string configPath)
        {
            var textData = File.ReadAllText(configPath);
            return JSON.Parse(textData).ToDictionary();
        }

        private void SetupNetworkManager(Dictionary<string, object> mainConfig)
        {
            var address = mainConfig.GetString("validator_ip");
            var port = mainConfig.GetInt("validator_port");
            NetworkManager.Instance.SetupManager(address, port);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using Extensions;
using UnityEngine;
using fastJSON;
using Loggers;
using Networking;

namespace Core
{
    public class StartController : MonoBehaviour, IController
    {
        [SerializeField] private StartReferences _startReferences;

        private Context _context;
        private Dictionary<string, object> _mainConfig;
        private void OnDestroy()
        {
            Deactivate();
        }

        private void Awake()
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
            CreateContext(_mainConfig);
        }

        private Dictionary<string, object> ReadMainConfig(string configPath)
        {
            var textData = File.ReadAllText(configPath);
            return JSON.Parse(textData).ToDictionary();
        }

        private void CreateContext(Dictionary<string, object> mainConfig)
        {
            _context = new Context(mainConfig);
        }
    }
}
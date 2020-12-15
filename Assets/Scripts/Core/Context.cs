using System;
using System.Collections.Generic;
using ElectronicVoting.Cryptography;
using ElectronicVoting.Electors;
using ElectronicVoting.Extensions;
using Factory;
using Networking;
using UnityEngine;

namespace Core
{
    public class Context : Singletone<Context>
    {
        public NetworkManager NetworkManager { get; }
        public Elector Elector { get; private set; } 
        public ICryptographyProvider CryptographyProvider { get; }
        public MainFactory MainFactory { get; }
        
        public Context(Dictionary<string, object> mainConfig)
        {
            if (_instance != null)
            {
                throw new Exception("More than one singletone");
            }

            fastJSON.JSON.Parameters.UseEscapedUnicode = true;
            _instance = this;
            CryptographyProvider = new RSACryptography();
            NetworkManager = new NetworkManager(mainConfig);
            MainFactory = new MainFactory();
            MainFactory.RegisterTypes();
        }

        public void InitializeElector(Dictionary<string, object> validatorKey)
        {
            Elector = new Elector(CryptographyProvider, validatorKey);
            Elector.CreateNewKeys();
            Debug.Log("Elector created");
            Debug.Log($"Validator's public key = {fastJSON.JSON.ToJSON(validatorKey)}");
            Debug.Log($"Elector's public key = {fastJSON.JSON.ToJSON(Elector.PublicSignKey.GetChangeableCopy())}");
        }
    }
}
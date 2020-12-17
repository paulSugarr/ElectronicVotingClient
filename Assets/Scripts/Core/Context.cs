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
            var checkCrypto = false;
            var i = 0;
            while (!checkCrypto)
            {
                Elector = new Elector(CryptographyProvider, validatorKey);
                Elector.CreateNewKeys();
                var blindedSigned = Elector.CreateBlindedSignedMessage(0);
                var blinded = Elector.CreateBlindedMessage(0);
                checkCrypto = CryptographyProvider.VerifyData(Elector.PublicSignKey.GetChangeableCopy(), blinded, blindedSigned);
                i++;
            }

            Debug.Log("Elector created");
        }
    }
}
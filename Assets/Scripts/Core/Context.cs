using System;
using System.Collections.Generic;
using Networking;

namespace Core
{
    public class Context : Singletone<Context>
    {
        public NetworkManager NetworkManager { get; }
        
        public Context(Dictionary<string, object> mainConfig) : base()
        {
            if (_instance != null)
            {
                throw new Exception("More than one singletone");
            }
            _instance = this;
            
            
            NetworkManager = new NetworkManager(mainConfig);
        }

        public void Initialize()
        {
            
        }
    }
}
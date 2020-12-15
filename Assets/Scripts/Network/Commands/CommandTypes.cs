using System;
using System.Collections.Generic;

namespace Networking.Commands
{
    public class CommandTypes
    {
        private readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();

        public void RegisterTypes()
        {
            _types.Add("log", typeof(LogCommand));
            _types.Add("send_validator_key", typeof(SendValidatorKeyCommand));
            _types.Add("set_validator_key", typeof(SetValidatorKeyCommand));
            _types.Add("set_elector_key", typeof(SetElectorKeyCommand));
            _types.Add("elector_blind_sign", typeof(SendValidatorBlindSignCommand));
            _types.Add("validator_sign", typeof(SendElectorSignedCommand));
        }
        public Type this[string id]
        {
            get { return _types[id]; }
        }
    }
}
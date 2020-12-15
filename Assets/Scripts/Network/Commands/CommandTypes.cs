﻿using System;
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
        }
        public Type this[string id]
        {
            get { return _types[id]; }
        }
    }
}
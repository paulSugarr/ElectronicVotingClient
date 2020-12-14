using System;
using System.Collections.Generic;
using Extensions;
using Networking.Commands;

namespace Factory
{
    public class CommandFactory : IFactory<ICommand>
    {

        private readonly CommandTypes _registeredTypes;

        public CommandFactory()
        {
            _registeredTypes = new CommandTypes();
        }
        public void RegisterTypes()
        {
            _registeredTypes.RegisterTypes();
        }
        public ICommand CreateInstance(params object[] args)
        {
            var type = (string) args[0];
            var info = args[1].ToDictionary();
            return CreateInstance(type, info);
        }
        public ICommand CreateInstance(string id, Dictionary<string, object> info)
        {
            var type = _registeredTypes[(string) info["type"]];
            return (ICommand) Activator.CreateInstance(type, id, info);
        }
    }
}
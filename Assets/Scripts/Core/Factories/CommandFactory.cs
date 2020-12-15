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
            var info = args[0].ToDictionary();
            return CreateInstance(info);
        }
        public ICommand CreateInstance(Dictionary<string, object> info)
        {
            var type = _registeredTypes[(string) info["type"]];
            return (ICommand) Activator.CreateInstance(type, info);
        }
    }
}
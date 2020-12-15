using System;
using System.Collections.Generic;
using Extensions;
using Networking.Commands;

namespace Factory
{
    public class MainFactory
    {

        private readonly Dictionary<Type, IFactory<object>> _factories;

        public MainFactory()
        {
            _factories = new Dictionary<Type, IFactory<object>>();
            
            _factories.Add(typeof(ICommand), new CommandFactory());
        }
        public T CreateInstance<T>(params object[] args)
        {
            var type = typeof(T);
            return (T) _factories[type].CreateInstance(args);
        }

        public void RegisterTypes()
        {
            foreach (var factory in _factories.Values)
            {
                factory.RegisterTypes();
            }
        }

    }
}
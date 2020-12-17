using System.Collections.Generic;

namespace Networking.Commands
{
    public interface ICommand
    {
        string Type { get; }
        Dictionary<string, object> GetInfo();
        void Execute();
    }
}
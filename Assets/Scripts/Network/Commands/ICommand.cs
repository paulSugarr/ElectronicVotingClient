using System.Collections.Generic;

namespace Networking.Commands
{
    public interface ICommand
    {
        string Type { get; }
        void Execute();
        Dictionary<string, object> GetInfo();
    }
}
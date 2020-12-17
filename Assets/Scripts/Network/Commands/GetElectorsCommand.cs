using System.Collections.Generic;

namespace Networking.Commands
{
    public class GetElectorsCommand : ICommand
    {
        public string Type { get; }

        public GetElectorsCommand()
        {
            Type = "get_electors";
        }

        public GetElectorsCommand(Dictionary<string, object> info)
        {
            Type = "get_electors";
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            return result;
        }

        public void Execute()
        {

        }
    }
}
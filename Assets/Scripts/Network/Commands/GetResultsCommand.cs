using System.Collections.Generic;

namespace Networking.Commands
{
    public class GetResultsCommand : ICommand
    {
        public string Type { get; }

        public GetResultsCommand()
        {
            Type = "get_results";
        }

        public GetResultsCommand(Dictionary<string, object> info)
        {
            Type = "get_results";
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
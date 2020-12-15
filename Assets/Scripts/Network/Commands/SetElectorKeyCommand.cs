using System.Collections.Generic;
using ElectronicVoting.Extensions;

namespace Networking.Commands
{
    public class SetElectorKeyCommand
    {
        public string Type { get; }
        public string Id { get; }
        public Dictionary<string, object> Key { get; }

        public SetElectorKeyCommand(string id, Dictionary<string, object> key)
        {
            Type = "set_elector_key";
            Id = id;
            Key = key;
        }
        public SetElectorKeyCommand(Dictionary<string, object> info)
        {
            Type = "set_elector_key";
            Id = info.GetString("id");
            Key = info.GetDictionary("key");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("id", Id);
            result.Add("key", Key);
            return result;
        }
        public void Execute()
        {

        }
    }
}
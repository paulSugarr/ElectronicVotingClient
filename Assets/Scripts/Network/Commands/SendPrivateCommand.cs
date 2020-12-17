using System.Collections.Generic;
using Extensions;

namespace Networking.Commands
{
    public class SendPrivateCommand : ICommand
    {
        public string Type { get; }
        public int Id { get; }
        public Dictionary<string, object> PrivateKey { get; }

        public SendPrivateCommand(Dictionary<string, object> info)
        {
            Type = "send_private";
            Id = info.GetInt("id");
            PrivateKey = info.GetDictionary("key");
        }

        public SendPrivateCommand(int id, Dictionary<string, object> privateKey)
        {
            Type = "send_private";
            Id = id;
            PrivateKey = privateKey;
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("id", Id);
            result.Add("key", PrivateKey);
            return result;
        }

        public void Execute()
        {
            
        }
    }
}
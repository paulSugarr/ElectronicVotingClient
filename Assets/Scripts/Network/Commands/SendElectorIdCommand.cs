using System.Collections.Generic;
using ElectronicVoting.Extensions;
using UnityEngine;

namespace Networking.Commands
{
    public class SendElectorIdCommand : ICommand
    {
        public string Type { get; }

        public int Id { get; }

        public SendElectorIdCommand(int id)
        {
            Type = "send_id";
            Id = id;
        }
        public SendElectorIdCommand(Dictionary<string, object> info)
        {
            Type = info.GetString("type");
            Id = info.GetInt("id");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("id", Id);
            return result;
        }

        public void Execute()
        {
            Debug.Log("ticket created");
        }
    }
}
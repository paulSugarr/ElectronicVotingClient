using System.Collections.Generic;
using Core;
using Extensions;
using UnityEngine;

namespace Networking.Commands
{
    public class SendElectorsCommand : ICommand
    {
        public string Type { get; }

        public int[] Electors { get; }

        public SendElectorsCommand(int[] electors)
        {
            Type = "electors";
            Electors = electors;
        }

        public SendElectorsCommand(Dictionary<string, object> info)
        {
            Type = "electors";
            Debug.Log(fastJSON.JSON.ToJSON(info));
            Debug.Log(info["electors"].GetType());
            Debug.Log(new List<int>().GetType());
            Electors = info.GetIntArray("electors");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("electors", Electors);
            return result;
        }

        public void Execute()
        {
            Context.Instance.Electors = Electors;
            Debug.Log($"electors voted: {Electors.Length}");
        }
    }
}
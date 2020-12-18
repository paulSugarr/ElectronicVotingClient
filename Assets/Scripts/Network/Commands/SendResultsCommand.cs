using System.Collections.Generic;
using Core;
using Extensions;
using UnityEngine;

namespace Networking.Commands
{
    public class SendResultsCommand : ICommand
    {
        public string Type { get; }

        public int[] Candidates { get; }

        public SendResultsCommand(int[] candidates)
        {
            Type = "candidates";
            Candidates = candidates;
        }

        public SendResultsCommand(Dictionary<string, object> info)
        {
            Type = "candidates";
            Candidates = info.GetIntArray("candidates");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("candidates", Candidates);
            return result;
        }

        public void Execute()
        {
            Debug.Log($"candidates amount = {Candidates.Length}");
            Context.Instance.Candidates = Candidates;
            Debug.Log($"First cand = {Context.Instance.Candidates[0]}");
        }
    }
}
using Core;
using Networking.Commands;
using UnityEngine;

namespace Networking
{
    public class RefreshResultsButton : MonoBehaviour
    {
        public void RefreshResults()
        {
            var command = new GetResultsCommand();
            Context.Instance.NetworkManager.SendCommandToAgency(command);
        }
    }
}
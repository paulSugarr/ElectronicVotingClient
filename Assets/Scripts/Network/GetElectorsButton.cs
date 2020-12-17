using Core;
using Networking.Commands;
using UnityEngine;

namespace Networking
{
    public class GetElectorsButton : MonoBehaviour
    {
        public void GetElectors()
        {
            var command = new GetElectorsCommand();
            Context.Instance.NetworkManager.SendCommandToAgency(command);
        }
        
    }
}
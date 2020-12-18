using Core;
using UnityEngine;

namespace UI
{
    public class ConnectButton : MonoBehaviour
    {
        public void Connect()
        {
            Context.Instance.NetworkManager.Connect();
            UIController.ShowLoadingScreen(true);
        }
    }
}
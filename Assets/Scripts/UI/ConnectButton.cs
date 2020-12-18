using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ConnectButton : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _iF;
        public void Connect()
        {
            Context.Instance.LoginId = _iF.text;
            Context.Instance.NetworkManager.Connect();
            UIController.ShowLoadingScreen(true);
        }
    }
}
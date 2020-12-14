using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class NetworkTest : MonoBehaviour
{

    private void Start()
    {
        Context.Instance.NetworkManager.Connect();
    }
    public void SendMessageToValidator(string message)
    {
        Context.Instance.NetworkManager.SendMessageToValidator(message);
    }
}

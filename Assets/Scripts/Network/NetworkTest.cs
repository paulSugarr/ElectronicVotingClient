using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Networking.Commands;

public class NetworkTest : MonoBehaviour
{

    private void Start()
    {
        Context.Instance.NetworkManager.Connect();
    }
    public void SendMessageToValidator()
    {
        // var commandInfo = new Dictionary<string, object>();
        // commandInfo.Add("type", "send_validator_key");
        var command = new SendValidatorKeyCommand();
        var commandData = command.GetInfo();
        Context.Instance.NetworkManager.SendMessageToValidator(fastJSON.JSON.ToJSON(commandData));
    }
}

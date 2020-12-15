using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using ElectronicVoting.Extensions;
using Networking.Commands;

public class ProtocolTest : MonoBehaviour
{

    private void Start()
    {
        Context.Instance.NetworkManager.Connect();
    }
    public void RequestValidatorKey()
    {
        // var commandInfo = new Dictionary<string, object>();
        // commandInfo.Add("type", "send_validator_key");
        var command = new SendValidatorKeyCommand();
        var commandData = command.GetInfo();
        Context.Instance.NetworkManager.SendMessageToValidator(fastJSON.JSON.ToJSON(commandData));
    }

    public void SendElectorKey()
    {
        var id = "paul";
        var key = Context.Instance.Elector.PublicSignKey;
        var command = new SetElectorKeyCommand(id, key.GetChangeableCopy());
        var commandData = command.GetInfo();
        Context.Instance.NetworkManager.SendMessageToValidator(fastJSON.JSON.ToJSON(commandData));
    }
}

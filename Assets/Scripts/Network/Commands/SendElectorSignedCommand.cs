using System.Collections.Generic;
using Core;
using ElectronicVoting.Extensions;
using UnityEngine;

namespace Networking.Commands
{
    public class SendElectorSignedCommand : ICommand
    {
        public string Type { get; }
        public byte[] Signed { get; }

        public SendElectorSignedCommand(byte[] signed)
        {
            Type = "validator_sign";
            Signed = signed;
        }
        public SendElectorSignedCommand(Dictionary<string, object> info)
        {
            Type = "validator_sign";
            Signed = System.Numerics.BigInteger.Parse(info.GetString("sign")).ToByteArray();
        }
        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("sign", new System.Numerics.BigInteger(Signed).ToString());
            return result;
        }

        public void Execute()
        {
            Debug.Log("validator signed");
            var signedByValidator = Signed;
            var elector = Context.Instance.Elector;
            var choiceIndex = elector.ChoiceIndex;
            
            var signedEncrypted = elector.RemoveBlindEncryption(signedByValidator);
            var encryptedBulletin = elector.GetEncryptedBulletin(choiceIndex);

            var command = new SendEncryptedCommand(0, encryptedBulletin, signedEncrypted);
            Context.Instance.NetworkManager.SendCommandToAgency(command);
        }
    }
}
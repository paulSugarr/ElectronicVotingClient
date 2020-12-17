using Core;
using ElectronicVoting.Extensions;
using Loggers;
using UnityEngine;

namespace Networking.Commands
{
    public class VoteTest : MonoBehaviour
    {
        private int _choiceIndex = 0;
        public void TestVote()
        {
            var elector = Context.Instance.Elector;
            var blinded = elector.CreateBlindedMessage(_choiceIndex);
            var blindedSigned = elector.CreateBlindedSignedMessage(_choiceIndex);
            
            var command = new SendValidatorBlindSignCommand(blinded, blindedSigned, "paul");
            Context.Instance.NetworkManager.SendCommandToValidator(command);
            
        }
    }
}
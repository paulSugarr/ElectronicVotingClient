using Core;
using ElectronicVoting.Extensions;
using Loggers;
using UnityEngine;

namespace Networking.Commands
{
    public class VoteTest : MonoBehaviour
    {
        [SerializeField] private int _choiceIndex = 0;
        public void TestVote()
        {
            var elector = Context.Instance.Elector;
            var blinded = elector.CreateBlindedMessage(_choiceIndex);
            var blindedSigned = elector.CreateBlindedSignedMessage(_choiceIndex);
            
            var command = new SendValidatorBlindSignCommand(blinded, blindedSigned, Context.Instance.LoginId);
            Context.Instance.NetworkManager.SendCommandToValidator(command);
            
        }
    }
}
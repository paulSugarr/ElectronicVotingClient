using System.Collections.Generic;

namespace Networking.Commands
{
    public class ElectorVoteCommand : ICommand
    {
        public string Type { get; }
        public byte[] Blinded { get; }
        public byte[] BlindedSigned { get; }

        public ElectorVoteCommand(byte[] blinded, byte[] blindedSigned)
        {
            Type = "elector_vote";
            Blinded = blinded;
            BlindedSigned = blindedSigned;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, object> GetInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using ElectronicVoting.Extensions;

namespace Networking.Commands
{
    public class SendEncryptedCommand : ICommand
    {
        public string Type { get; }
        private byte[] Encrypted { get; }
        private byte[] EncryptedSigned { get; }
        public Dictionary<string, object> EncryptionKey { get; }


        public SendEncryptedCommand(byte[] encrypted, byte[] encryptedSigned, Dictionary<string, object> publicKey)
        {
            Type = "elector_encrypt_sign";
            EncryptionKey = publicKey;
            Encrypted = encrypted;
            EncryptedSigned = encryptedSigned;
        }
        public SendEncryptedCommand(Dictionary<string, object> info)
        {
            Type = "elector_encrypt_sign";
            Encrypted = System.Numerics.BigInteger.Parse(info.GetString("encrypted")).ToByteArray();
            EncryptedSigned = System.Numerics.BigInteger.Parse(info.GetString("signed")).ToByteArray();
            EncryptionKey = info.GetDictionary("key");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("encrypted", new System.Numerics.BigInteger(Encrypted).ToString());
            result.Add("signed", new System.Numerics.BigInteger(EncryptedSigned).ToString());
            result.Add("key", EncryptionKey);
            return result;
        }
        public void Execute()
        {
            
        }

    }
}
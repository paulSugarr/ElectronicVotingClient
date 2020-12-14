using System.Collections.Generic;

namespace ElectronicVoting.Cryptography
{
    public interface ICryptographyProvider
    {
        byte[] Encrypt(Dictionary<string, object> publicKey, byte[] data);
        byte[] Decrypt(Dictionary<string, object> privateKey, byte[] data);
        byte[] SignData(Dictionary<string, object> privateKey, byte[] data);
        bool VerifyData(Dictionary<string, object> publicKey, byte[] data, byte[] signedData);
        byte[] BlindData(Dictionary<string, object> blindKey, Dictionary<string, object> signKey, byte[] data);
        byte[] UnBlindData(Dictionary<string, object> blindKey, Dictionary<string, object> signKey, byte[] blindedData);
        IKeyCreator KeyCreator { get; }
    }
}
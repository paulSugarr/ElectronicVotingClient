using System.Collections.Generic;

namespace ElectronicVoting.Cryptography
{
    public interface IKeyCreator
    {
        Dictionary<string, object> CreatePrivateKey();
        Dictionary<string, object> CreatePublicKey(Dictionary<string, object> privateKey);
        Dictionary<string, object> CreateBlindKey();
    }
}
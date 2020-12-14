using System.Collections.Generic;
using ElectronicVoting.Cryptography;
using ElectronicVoting.Extensions;

namespace ElectronicVoting.Validators
{
    public class Validator
    {
        public IReadOnlyDictionary<string, object> PublicKey => _publicKey;
        
        private Dictionary<string, object> _privateKey;
        private Dictionary<string, object> _publicKey;
        private ICryptographyProvider _cryptographyProvider;

        public Validator(ICryptographyProvider cryptographyProvider)
        {
            _cryptographyProvider = cryptographyProvider;
        }
        public Validator(ICryptographyProvider cryptographyProvider, Dictionary<string, object> initialData)
        {
            _cryptographyProvider = cryptographyProvider;
            _privateKey = initialData.GetDictionary("private_key");
            _publicKey = initialData.GetDictionary("public_key");
        }

        public void CreateKeys()
        {
            _privateKey = _cryptographyProvider.KeyCreator.CreatePrivateKey();
            _publicKey = _cryptographyProvider.KeyCreator.CreatePublicKey(_privateKey);
        }

        public bool VerifyBulletin(byte[] signedBlindedData, byte[] blindedData, Dictionary<string, object> publicElectorKey)
        {
            return _cryptographyProvider.VerifyData(publicElectorKey, blindedData, signedBlindedData);
        }

        public byte[] SignBulletin(byte[] blindedData)
        {
            return _cryptographyProvider.SignData(_privateKey, blindedData);
        }
    }
}
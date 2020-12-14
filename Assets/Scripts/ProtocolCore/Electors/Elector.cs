using System.Collections.Generic;
using System.Text;
using ElectronicVoting.Cryptography;
using ElectronicVoting.Extensions;

namespace ElectronicVoting.Electors
{
    public class Elector
    {
        public IReadOnlyDictionary<string, object> PublicSignKey => _publicSignKey;
        
        private Dictionary<string, object> _validatorPublicKey;
        private Dictionary<string, object> _privateKey;
        private Dictionary<string, object> _publicEncryptionKey;
        private Dictionary<string, object> _publicSignKey;
        private Dictionary<string, object> _blindKey;

        private ICryptographyProvider _cryptographyProvider;

        public Elector(ICryptographyProvider cryptographyProvider, Dictionary<string, object> validatorPublicKey)
        {
            _cryptographyProvider = cryptographyProvider;
            _validatorPublicKey = validatorPublicKey;
        }
        public Elector(ICryptographyProvider cryptographyProvider, Dictionary<string, object> initialData, Dictionary<string, object> validatorPublicKey)
        {
            _cryptographyProvider = cryptographyProvider;
            _privateKey = initialData.GetDictionary("private_key");
            _publicEncryptionKey = initialData.GetDictionary("public_encrypt_key");
            _publicSignKey = initialData.GetDictionary("public_sign_key");
            _blindKey = initialData.GetDictionary("blind_key");
            _validatorPublicKey = validatorPublicKey;
        }

        public void CreateNewKeys()
        {
            _privateKey = _cryptographyProvider.KeyCreator.CreatePrivateKey();
            _publicEncryptionKey = _cryptographyProvider.KeyCreator.CreatePublicKey(_privateKey);
            _publicSignKey = _cryptographyProvider.KeyCreator.CreatePublicKey(_privateKey);
            _blindKey = _cryptographyProvider.KeyCreator.CreateBlindKey();
        }
        
        /// <summary> Step 2 in E-voting protocol</summary>
        public byte[] CreateBlindedSignedMessage(int choiceIndex)
        {
            var blinded = CreateBlindedMessage(choiceIndex);
            var signed = _cryptographyProvider.SignData(_privateKey, blinded);
            return signed;
        }

        public byte[] CreateBlindedMessage(int choiceIndex)
        {
            var encryptB = GetEncryptedBulletin(choiceIndex);
            var blindEncryptB = _cryptographyProvider.BlindData(_blindKey, _validatorPublicKey, encryptB);
            return blindEncryptB;
        }
        
        /// <summary> Step 4 in E-voting protocol</summary>
        public byte[] RemoveBlindEncryption(byte[] blindedMessage)
        {
            var result = _cryptographyProvider.UnBlindData(_blindKey, _validatorPublicKey, blindedMessage);
            return result;
        }
        
        /// <summary> Step 6 in E-voting protocol</summary>
        public Dictionary<string, object> GetPrivateKey()
        {
            return _privateKey;
        }

        public byte[] GetEncryptedBulletin(int choiceIndex)
        {
            var bulletin = CreateBulletin(choiceIndex);
            var data = Encoding.UTF8.GetBytes(bulletin);
            var encryptB = _cryptographyProvider.Encrypt(_publicEncryptionKey, data);
            return encryptB;
        }

        public byte[] GetSignedEncryptedBulletin(int choiceIndex)
        {
            var encrypted = GetEncryptedBulletin(choiceIndex);
            return _cryptographyProvider.SignData(_privateKey, encrypted);
        }
        private string CreateBulletin(int choiceIndex)
        {
            var bulletin = choiceIndex.ToString();
            return bulletin;
        }
    }
}
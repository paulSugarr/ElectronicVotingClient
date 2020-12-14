using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElectronicVoting.Extensions;
using BigInt = System.Numerics.BigInteger;

namespace ElectronicVoting.Cryptography
{
    public class RSACryptography : ICryptographyProvider
    {
        public IKeyCreator KeyCreator { get; }

        public RSACryptography()
        {
            KeyCreator = new RSAKeyCreator();
        }

        public byte[] Encrypt(Dictionary<string, object> publicKey, byte[] data)
        {
            var e = BigInt.Parse(publicKey.GetString("e"));
            var n = BigInt.Parse(publicKey.GetString("n"));

            var m = new BigInt(data);

            var c = BigInt.ModPow(m, e, n);
            var result = c.ToByteArray();
            return result;
        }

        public byte[] Decrypt(Dictionary<string, object> privateKey, byte[] data)
        {
            var d = BigInt.Parse(privateKey.GetString("d"));
            var n = BigInt.Parse(privateKey.GetString("n"));

            var c = new BigInt(data);

            var m = BigInt.ModPow(c, d, n);
            var result = m.ToByteArray();
            return result;
        }

        public byte[] SignData(Dictionary<string, object> privateKey, byte[] data)
        {
            var d = BigInt.Parse(privateKey.GetString("d"));
            var n = BigInt.Parse(privateKey.GetString("n"));

            var m = new BigInt(data);

            var s = BigInt.ModPow(m, d, n);
            var result = s.ToByteArray();
            return result;
        }

        public bool VerifyData(Dictionary<string, object> publicKey, byte[] data, byte[] signedData)
        {
            var e = BigInt.Parse(publicKey.GetString("e"));
            var n = BigInt.Parse(publicKey.GetString("n"));

            var s = new BigInt(signedData);

            var m = BigInt.ModPow(s, e, n);
            var result = m.ToByteArray();
            
            return result.SequenceEqual(data);
        }

        public byte[] UnBlindData(Dictionary<string, object> blindKey, Dictionary<string, object> signKey, byte[] blindedData)
        {
            var r = BigInt.Parse(blindKey.GetString("r"));
            var e = BigInt.Parse(signKey.GetString("e"));
            var n = BigInt.Parse(signKey.GetString("n"));

            var inversedR = r.GetInversed(n);
            var m = new BigInt(blindedData);
            var result = BigInt.ModPow(m * inversedR, 1, n);
            return result.ToByteArray();
        }

        public byte[] BlindData(Dictionary<string, object> blindKey, Dictionary<string, object> signKey, byte[] data)
        {
            var r = BigInt.Parse(blindKey.GetString("r"));
            var e = BigInt.Parse(signKey.GetString("e"));
            var n = BigInt.Parse(signKey.GetString("n"));
            var multiplier = BigInt.Pow(r, (int) e);
            var m = new BigInt(data);
            var result = BigInt.ModPow(m * multiplier, 1, n);
            return result.ToByteArray();
        }
    }
}
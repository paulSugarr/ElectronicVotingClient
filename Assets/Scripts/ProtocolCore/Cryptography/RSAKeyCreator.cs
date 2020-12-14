using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using CoreBigInt = BigInteger.NetCore.BigInteger;
using BigInt = System.Numerics.BigInteger;


namespace ElectronicVoting.Cryptography
{
    public class RSAKeyCreator : IKeyCreator
    {
        public Dictionary<string, object> CreatePrivateKey()
        {
            var rand = new Random(DateTimeOffset.UtcNow.Millisecond);
            var pp = RandomPrime(1024, rand);
            var qq = RandomPrime(1024, rand);

            var p = BigInt.Parse(pp.ToString());
            var q = BigInt.Parse(qq.ToString());
            // Console.WriteLine($"p = {p}");
            // Console.WriteLine($"q = {q}");
            var n = q * p;
            var phi = (p - 1) * (q - 1);
            var e = RandomPrime(8, rand);
            Gcd(e, phi, out var x, out var y);

            var d = x > 0 ? x : x + phi;
            
            // Console.WriteLine($"phi = {phi}");
            // Console.WriteLine($"x = {x}");
            // Console.WriteLine($"y = {y}");
            // Console.WriteLine($"d = {d}");


            var result = new Dictionary<string, object>();
            result.Add("p", p.ToString());
            result.Add("q", q.ToString());
            result.Add("n", n.ToString());
            result.Add("phi", phi.ToString());
            result.Add("e", e.ToString());
            result.Add("d", d.ToString());
            return result;
        }
        public Dictionary<string, object> CreatePublicKey(Dictionary<string, object> privateKey)
        {
            var result = new Dictionary<string, object>();
            result.Add("n", privateKey["n"]);
            result.Add("e", privateKey["e"]);
            return result;
        }

        public Dictionary<string, object> CreateBlindKey()
        {
            var rand = new Random(DateTimeOffset.UtcNow.Millisecond);
            var r = rand.Next(1000000, 5000000);
            var result = new Dictionary<string, object>();
            result.Add("r", r.ToString());
            return result;
        }
        private static BigInt Gcd(BigInt a, BigInt b, out BigInt x, out BigInt y)
        {
            if (b < a)
            {
                var t = a;
                a = b;
                b = t;
            }
    
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
 
            var gcd = Gcd(b % a, a, out x, out y);
    
            var newY = x;
            var newX = y - (b / a) * x;
    
            x = newX;
            y = newY;

            return gcd;
        }

        private static BigInt RandomPrime(int size, Random random)
        {
            while (true)
            {
                var result = new CoreBigInt(size, random);
                if (result.IsProbablePrime(120)) return BigInt.Parse(result.ToString());
            }
        }
    }
}
using BigInt = System.Numerics.BigInteger;

namespace ElectronicVoting.Extensions
{
    public static class BigIntegerExtensions
    {
        public static BigInt GetInversed(this BigInt target, BigInt modulus)
        {
            Gcd(target, modulus, out var x, out var y);

            var inversed = x > 0 ? x : x + modulus;
            return inversed;
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
    }
}
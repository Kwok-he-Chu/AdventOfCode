namespace AOC2025 ;

    public class Extensions
    {
        public static long GreatestCommonDivisor(long a, long b) // GCD
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public static long LeastCommonMultiple(long a, long b) // LCM
        {
            return a / GreatestCommonDivisor(a, b) * b;
        }
    }
    
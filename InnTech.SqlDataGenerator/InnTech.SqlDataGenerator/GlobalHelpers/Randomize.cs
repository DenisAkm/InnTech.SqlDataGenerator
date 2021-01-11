using System;

namespace InnTech.SqlDataGenerator
{
    public static class Randomize
    {
        private static readonly Random Rand = new Random();

        public static int Next(int maxValue)
        {
            return Rand.Next(maxValue);
        }

        public static double NextDouble(double minValue, double maxValue)
        {
            return Rand.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static float NextFloat(float minValue, float maxValue)
        {
            return (float)NextDouble(minValue, maxValue);
        }

        public static byte[] NextBytes(int size)
        {
            var bytes = new byte[size];
            Rand.NextBytes(bytes);
            return bytes;
        }

        public static int Next(int minValue, int maxValue)
        {
            return Rand.Next(minValue, maxValue);
        }

        public static decimal NextDecimal(decimal minValue, decimal maxValue)
        {
            var value = Rand.NextDouble();
            return (maxValue - minValue) * (decimal)value + minValue;
        }

        private static int NextInt32(this Random rng)
        {
            var firstBits = rng.Next(0, 1 << 4) << 28;
            var lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }
    }
}

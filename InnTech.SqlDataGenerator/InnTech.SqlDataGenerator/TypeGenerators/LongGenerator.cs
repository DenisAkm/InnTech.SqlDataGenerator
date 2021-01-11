namespace InnTech.SqlDataGenerator
{
    public class LongGenerator : ITypeGenerator
    {
        public long MinValue { get; set; }
        public long MaxValue { get; set; }

        public LongGenerator(long minValue, long maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public object GetRandom(EntityProperty column)
        {
            long result = Randomize.Next((int)(MinValue >> 32), (int)(MaxValue >> 32));
            result = result << 32;
            result |= Randomize.Next((int)MinValue, (int)MaxValue);
            return result;
        }

        public string GetValue(EntityProperty column)
        {
            return $"{GetRandom(column)}";
        }
    }
}
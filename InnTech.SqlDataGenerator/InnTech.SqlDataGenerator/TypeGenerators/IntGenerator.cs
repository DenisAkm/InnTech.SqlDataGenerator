namespace InnTech.SqlDataGenerator
{
    public class IntGenerator : ITypeGenerator
    {
        private int MinValue { get; }
        private int MaxValue { get; }

        public IntGenerator(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public object GetRandom(EntityProperty column)
        {
            return Randomize.Next(MinValue, MaxValue);
        }

        public string GetValue(EntityProperty column)
        {
            return $"{GetRandom(column)}";
        }
    }
}
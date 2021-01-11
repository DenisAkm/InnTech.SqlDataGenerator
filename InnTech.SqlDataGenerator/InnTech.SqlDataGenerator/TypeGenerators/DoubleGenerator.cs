namespace InnTech.SqlDataGenerator
{
    public class DoubleGenerator : ITypeGenerator
    {
        private double MinValue { get; set; }

        public double MaxValue { get; set; }

        public DoubleGenerator(double minValue, double maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
        public object GetRandom(EntityProperty column)
        {
            return Randomize.NextDouble(MinValue, MaxValue);
        }

        public string GetValue(EntityProperty column)
        {
            return $"'{GetRandom(column)}'";
        }
    }
}
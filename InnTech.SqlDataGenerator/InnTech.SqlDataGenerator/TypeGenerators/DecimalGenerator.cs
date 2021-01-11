namespace InnTech.SqlDataGenerator
{
    public class DecimalGenerator : ITypeGenerator
    {
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public DecimalGenerator(decimal minValue, decimal maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public object GetRandom(EntityProperty column)
        {
            return Randomize.NextDecimal(MinValue, MaxValue);
        }

        public string GetValue(EntityProperty column)
        {
            return $"{GetRandom(column)}".Replace(",", ".");
        }
    }
}
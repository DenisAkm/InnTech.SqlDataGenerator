namespace InnTech.SqlDataGenerator
{
    public class FloatGenerator : ITypeGenerator
    {
        private float MinFloat { get; set; }

        public float MaxFloat { get; set; }

        public FloatGenerator(float minFloat, float maxFloat)
        {
            MinFloat = minFloat;
            MaxFloat = maxFloat;
        }
        public object GetRandom(EntityProperty column)
        {
            return Randomize.NextFloat(MinFloat, MaxFloat);
        }

        public string GetValue(EntityProperty column)
        {
            return $"'{GetRandom(column)}'";
        }
    }
}
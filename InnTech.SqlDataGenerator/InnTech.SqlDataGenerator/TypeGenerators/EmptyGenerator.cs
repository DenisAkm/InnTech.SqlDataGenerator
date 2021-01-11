namespace InnTech.SqlDataGenerator
{
    public class EmptyGenerator : ITypeGenerator
    {
        public object GetRandom(EntityProperty column)
        {
            return null;
        }

        public string GetValue(EntityProperty column)
        {
            return string.Empty;
        }
    }
}
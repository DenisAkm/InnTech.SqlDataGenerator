namespace InnTech.SqlDataGenerator
{
    public interface ITypeGenerator
    {
        public object GetRandom(EntityProperty column);

        public string GetValue(EntityProperty column);
    }
}
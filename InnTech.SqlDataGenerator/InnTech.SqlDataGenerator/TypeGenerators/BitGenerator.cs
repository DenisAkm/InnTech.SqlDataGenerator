using System;

namespace InnTech.SqlDataGenerator
{
    internal class BitGenerator : ITypeGenerator
    {
        public object GetRandom(EntityProperty column)
        {
            return Convert.ToBoolean(Randomize.Next(1));
        }
        public string GetValue(EntityProperty column)
        {
            return $"'{GetRandom(column)}'";
        }
    }
}

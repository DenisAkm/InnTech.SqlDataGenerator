using System.Data;

namespace InnTech.SqlDataGenerator
{
    public class EntityProperty
    {
        public string Name { get; set; }
        public SqlDbType Type { get; set; }
        public string Value { get; set; }
        public string ReferenceEntity { get; set; }
    }
}
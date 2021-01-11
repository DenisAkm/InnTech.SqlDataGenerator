using System;
using System.Linq;

namespace InnTech.SqlDataGenerator
{
    public class GuidGenerator : ITypeGenerator
    {
        public ISqlServerUtils Client;
        public GeneratorSettings Options;
        public GuidGenerator(ISqlServerUtils client, GeneratorSettings options)
        {
            Client = client;
            Options = options;
        }

        public object GetRandom(EntityProperty column)
        {
            if (column.Name == "Id") return Guid.NewGuid();

            var referenceEntity = column.Name switch
            {
                "CreatedById" => "Contact",
                "ModifiedById" => "Contact",
                _ => column.ReferenceEntity
            };

            var guidCollection = DataGeneratorCore.CreatedEntities
                                    .Where(x => x.EntityName == referenceEntity)
                                    .Select(x => x.Id)
                                    .ToList();
            var dbGuidCollection = Client.GetEntityIdCollection(referenceEntity);
            guidCollection.AddRange(dbGuidCollection);

            if (guidCollection.Count > 0)
            {
                return guidCollection[Randomize.Next(0, guidCollection.Count)];
            }
            else
            {
                return Guid.Empty;
            }
        }

        public string GetValue(EntityProperty column)
        {
            var value = GetRandom(column).ToString();
            return value == Guid.Empty.ToString() ? string.Empty : $"'{value}'";
        }
    }
}
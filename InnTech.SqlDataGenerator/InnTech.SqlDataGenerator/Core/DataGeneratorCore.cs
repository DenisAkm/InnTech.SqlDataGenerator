using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InnTech.SqlDataGenerator
{
    public class DataGeneratorCore
    {
        public GeneratorSettings Options;
        public ISqlServerUtils Client;

        StringBuilder sqlBuilder = new StringBuilder(sqlInitPart);
        Queue<string> EntityQueue = new Queue<string>();

        const string sqlInitPart = "SET NOCOUNT ON;\nSET XACT_ABORT ON;\nBEGIN TRAN\n";
        const string sqlEndPart = "COMMIT TRAN";

        public static List<EntityModel> CreatedEntities = new List<EntityModel>();

        public DataGeneratorCore(ISqlServerUtils client, GeneratorSettings options)
        {
            Options = options;
            Client = client;
        }

        public StringBuilder BuildSqlScript(Dictionary<string, int> entitiesToCreate)
        {
            try
            {
                foreach (var entityToCreate in entitiesToCreate)
                {
                    EntityQueue.Enqueue(entityToCreate.Key);
                }

                int enqueueCounter = 0;
                while (EntityQueue.Count > 0)
                {
                    string entityName = EntityQueue.Dequeue();
                    int count = entitiesToCreate[entityName];

                    using (EntityModel originEntity = GetEmptyEntity(entityName))
                    {
                        for (int i = 0; i < count; i++)
                        {
                            var entity = originEntity.Copy();

                            if (!SetAllValues(entity) && enqueueCounter <= EntityQueue.Count)
                            {
                                EntityQueue.Enqueue(entityName);
                                enqueueCounter++;
                                break;
                            }
                            else
                            {
                                var sqlExpression = CreateEntitySql(entity);
                                CreatedEntities.Add(entity);

                                sqlBuilder.Append(sqlExpression);
                                sqlBuilder.Append(Environment.NewLine);

                                if (i == count - 1)
                                {
                                    enqueueCounter = 0;
                                }
                            }
                        }

                    }
                }

                return sqlBuilder.Append(sqlEndPart);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string CreateEntitySql(EntityModel entity)
        {
            var insertValues = new Dictionary<string, string>();
            foreach (var property in entity)
            {
                if (string.IsNullOrEmpty(property.Value)) continue;
                insertValues.Add(property.Name, property.Value);
            }

            return Client.GetInsertSqlScript(entity.EntityName, insertValues);
        }

        private EntityModel GetEmptyEntity(string entityName)
        {
            var entity = new EntityModel(entityName);

            var propertiesTypes = Client.GetPropertiesTypes(entityName);
            foreach (var (colName, colType) in propertiesTypes)
            {
                if (!Enum.TryParse(colType, true, out SqlDbType sqlDataType)) continue;

                var columnProperty = new EntityProperty
                {
                    Name = colName,
                    Type = sqlDataType
                };
                entity.Add(columnProperty);
            }

            var allReferences = Client.GetAllReferences(entity.EntityName);
            foreach (var prop in entity)
            {
                var referenceEntity =
                    allReferences.Where(x => x.Key == prop.Name)
                        .Select(x => x.Value).FirstOrDefault();

                if (referenceEntity == default) continue;

                prop.ReferenceEntity = referenceEntity;
            }

            return entity;
        }

        public bool SetAllValues(EntityModel entity)
        {
            bool allSucceded = true;

            foreach (var column in entity)
            {
                if (Options.CustomValues.Any(x => x.Key == $"{entity.EntityName}.{column.Name}"))
                {
                    column.Value = Options.CustomValues.First(x => x.Key == $"{entity.EntityName}.{column.Name}").Value;
                }
                else
                {
                    var generator = GetSqlTypeGenerator(column);
                    column.Value = generator.GetValue(column);

                    if (string.IsNullOrEmpty(column.Value))
                    {
                        allSucceded = false;
                    }
                }
            }
            return allSucceded;
        }

        private ITypeGenerator GetSqlTypeGenerator(EntityProperty column)
        {
            return column.Type switch
            {
                SqlDbType.UniqueIdentifier => new GuidGenerator(Client, Options),
                SqlDbType.NVarChar => new StringGenerator(Options.Vocabulary, Options.NVarCharLength),
                SqlDbType.DateTime2 => new DateTimeGenerator(Options.StartDateTime2, Options.FinishDateTime2, Options.SetDateTimeNow.Any(x => x == column.Name)),
                SqlDbType.Int => new IntGenerator(Options.MinInt, Options.MaxInt),
                SqlDbType.Bit => new BitGenerator(),
                SqlDbType.BigInt => new LongGenerator(Options.MinBigInt, Options.MaxBigInt),
                SqlDbType.Date => new DateTimeGenerator(Options.StartDate, Options.FinishDate, Options.SetDateTimeNow.Any(x => x == column.Name)),
                SqlDbType.VarBinary => new ByteArrayGenerator(Options.VarBinarySize),
                SqlDbType.Time => new TimeSpanGenerator(Options.MinTime, Options.MaxTime),
                SqlDbType.Float => new DoubleGenerator(Options.MinFloat, Options.MaxFloat),
                SqlDbType.Binary => new ByteArrayGenerator(Options.BinarySize),
                SqlDbType.Real => new FloatGenerator(Options.MinReal, Options.MaxReal),
                SqlDbType.DateTime => new DateTimeGenerator(Options.StartDateTime, Options.FinishDateTime, Options.SetDateTimeNow.Any(x => x == column.Name)),
                SqlDbType.Decimal => new DecimalGenerator(Options.MinDecimal, Options.MaxDecimal),
                SqlDbType.Text => new StringGenerator(Options.Vocabulary, Options.TextLength),
                SqlDbType.SmallDateTime => new DateTimeGenerator(Options.StartSmallDateTime, Options.FinishSmallDateTime, Options.SetDateTimeNow.Any(x => x == column.Name)),
                SqlDbType.Char => new StringGenerator(Options.Vocabulary, Options.CharLength),
                SqlDbType.NText => new StringGenerator(Options.Vocabulary, Options.NTextLength),
                SqlDbType.VarChar => new StringGenerator(Options.Vocabulary, Options.VarCharLength),
                SqlDbType.Money => new DecimalGenerator(Options.MinMoney, Options.MaxMoney),
                SqlDbType.NChar => new StringGenerator(Options.Vocabulary, Options.NCharLength),
                SqlDbType.TinyInt => new IntGenerator(Options.MinTinyInt, Options.MaxTinyInt),
                SqlDbType.Timestamp => new ByteArrayGenerator(Options.TimestampSize),
                SqlDbType.SmallInt => new IntGenerator(Options.MinSmallInt, Options.MaxSmallInt),
                SqlDbType.SmallMoney => new DecimalGenerator(Options.MinSmallMoney, Options.MaxSmallMoney),
                SqlDbType.DateTimeOffset => new DateTimeOffsetGenerator(Options.StartDateTimeOffset, Options.FinishDateTimeOffset, Options.SetDateTimeNow.Any(x => x == column.Name)),

                SqlDbType.Xml => new EmptyGenerator(),
                SqlDbType.Image => new EmptyGenerator(),
                SqlDbType.Variant => new EmptyGenerator(),
                SqlDbType.Udt => new EmptyGenerator(),
                SqlDbType.Structured => new EmptyGenerator(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
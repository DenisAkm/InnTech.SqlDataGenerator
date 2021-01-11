using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace InnTech.SqlDataGenerator
{
    public class MssqlConnectionUtils : ISqlServerUtils
    {
        private SqlConnection SqlConnection { get; }

        public MssqlConnectionUtils(string connectionString)
        {
            SqlConnection = new SqlConnection(connectionString);
        }

        public virtual List<Guid> GetEntityIdCollection(string entityName)
        {
            var lookUpValues = new List<Guid>();
            if (string.IsNullOrEmpty(entityName)) return lookUpValues;

            var getLookUpValuesSqlQuery = $"SELECT Id FROM {entityName}";

            var connectionClosed = SqlConnection.State == System.Data.ConnectionState.Closed;
            if (connectionClosed)
            {
                SqlConnection.Open();
            }
            var sqlCommand = new SqlCommand(getLookUpValuesSqlQuery, SqlConnection);
            var sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var id = Guid.Parse(sqlDataReader.GetValue(0).ToString());
                    lookUpValues.Add(id);
                }
            }
            if (connectionClosed)
            {
                SqlConnection.Close();
            }
            return lookUpValues;
        }

        public virtual Dictionary<string, string> GetAllReferences(string entityName)
        {
            var getReferenciesSqlQuery =
                @$"SELECT 
                   COL_NAME(fc.parent_object_id, fc.parent_column_id) ColName,
                   OBJECT_NAME(f.referenced_object_id) TableName
                FROM
                   sys.foreign_keys AS f
                INNER JOIN
                   sys.foreign_key_columns AS fc
                      ON f.OBJECT_ID = fc.constraint_object_id
                INNER JOIN
                   sys.tables t
                      ON t.OBJECT_ID = fc.referenced_object_id
                WHERE
                   OBJECT_NAME(f.parent_object_id) = '{entityName}'";

            var referencies = new Dictionary<string, string>();
            var connectionClosed = SqlConnection.State == System.Data.ConnectionState.Closed;

            if (connectionClosed)
            {
                SqlConnection.Open();
            }
            var sqlCommand = new SqlCommand(getReferenciesSqlQuery, SqlConnection);
            var sqlDataReader = sqlCommand.ExecuteReader();
            if (!sqlDataReader.HasRows) return referencies;
            while (sqlDataReader.Read())
            {
                referencies.Add(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString());
            }

            if (connectionClosed)
            {
                SqlConnection.Close();
            }
            return referencies;
        }

        public virtual Dictionary<string, string> GetPropertiesTypes(string entityName)
        {
            var sqlExpression =
                @$"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS
                  WHERE TABLE_NAME = '{entityName}'";

            var entityInfo = new Dictionary<string, string>();

            var connectionClosed = SqlConnection.State == System.Data.ConnectionState.Closed;

            if (connectionClosed)
            {
                SqlConnection.Open();
            }
            var sqlCommand = new SqlCommand(sqlExpression, SqlConnection);
            var sqlDataReader = sqlCommand.ExecuteReader();
            if (!sqlDataReader.HasRows) return entityInfo;
            while (sqlDataReader.Read())
            {
                entityInfo.Add(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString());
            }
            if (connectionClosed)
            {
                SqlConnection.Close();
            }
            return entityInfo;
        }

        public virtual string GetInsertSqlScript(string entityName, Dictionary<string, string> insertValues)
        {
            var columnNames = insertValues.Select(x => $"[{x.Key}]");
            var columnValues = insertValues.Select(x => x.Value);

            var columns = string.Join(", ", columnNames);
            var values = string.Join(", ", columnValues);
            var exp = $"IF NOT EXISTS (SELECT TOP 1 Id FROM {entityName} WHERE Id = {insertValues["Id"]})\n" +
                       $"BEGIN\n" +
                       $"INSERT INTO {entityName} ({columns}) VALUES ({values})\n" +
                       $"END\n";
            return exp;
        }
    }
}
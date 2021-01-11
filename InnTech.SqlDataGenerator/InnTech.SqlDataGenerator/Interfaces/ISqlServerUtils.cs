using System;
using System.Collections.Generic;

namespace InnTech.SqlDataGenerator
{
    public interface ISqlServerUtils
    {
        Dictionary<string, string> GetAllReferences(string entityName);

        Dictionary<string, string> GetPropertiesTypes(string entityName);

        List<Guid> GetEntityIdCollection(string entityName);

        string GetInsertSqlScript(string entityName, Dictionary<string, string> insertValues);
    }
}
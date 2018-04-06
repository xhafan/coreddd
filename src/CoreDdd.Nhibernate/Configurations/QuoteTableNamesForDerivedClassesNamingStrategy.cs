using System;
using NHibernate.Cfg;

namespace CoreDdd.Nhibernate.Configurations
{
    // table names for derived classes are not quoted for PostgreSQL so this naming strategy needs to be applied for PostgreSQL DB.
    public class QuoteTableNamesForDerivedClassesNamingStrategy : INamingStrategy
    {
        public string ClassToTableName(string className)
        {
            var indexOfLastDot = className.LastIndexOf(".", StringComparison.Ordinal);
            className = className.Substring(indexOfLastDot + 1);
            return "\"" + className.Replace("`", "") + "\"";
        }

        public string PropertyToColumnName(string propertyName)
        {
            return propertyName;
        }

        public string TableName(string tableName)
        {
            return "\"" + tableName.Replace("`", "") + "\"";
        }

        public string ColumnName(string propertyName)
        {
            return propertyName;
        }

        public string PropertyToTableName(string className, string propertyName)
        {
            return null;
        }

        public string LogicalColumnName(string columnName, string propertyName)
        {
            return null;
        }
    }
}
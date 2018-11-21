using System;
using NHibernate.Cfg;

namespace CoreDdd.Nhibernate.Configurations
{
    // Double quote identifiers to make sure reserved keywords would not cause sql errors.
    // https://stackoverflow.com/a/2901499/379279
    internal class QuoteTableNamesForDerivedClassesNamingStrategy : INamingStrategy // todo: rename to DoubleQuoteIdentifiersNamingStrategy
    {
        public string ClassToTableName(string className)
        {
            // PostgreSQL generates a full class name as a table name for derived classes 
            // which throws a SQL error: 42601: improper qualified name (too many dotted names)
            return _ExtractClassNameFromFullClassName(className);
        }

        private string _ExtractClassNameFromFullClassName(string fullClassName)
        {
            var indexOfLastDot = fullClassName.LastIndexOf(".", StringComparison.Ordinal);
            fullClassName = fullClassName.Substring(indexOfLastDot + 1);
            return $"\"{fullClassName.Replace("`", "")}\"";
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
            return $"\"{propertyName}\"";
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
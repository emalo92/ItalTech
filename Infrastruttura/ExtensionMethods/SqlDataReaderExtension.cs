using Microsoft.Data.SqlClient;
using System;

namespace Infrastruttura.ExtensionMethods
{
    public static class SqlDataReaderExtension
    {

        public static string GetSafeString(this SqlDataReader reader, string fieldName)
        {
            ContainValue(reader, fieldName, out object val);
            return val?.ToString().Trim();
        }

        public static decimal? GetSafeDecimal(this SqlDataReader reader, string fieldName, bool isNullable = false)
        {
            if (!ContainValue(reader, fieldName, out object val))
                return isNullable ? (decimal?)null : 0;
            return decimal.Parse(val.ToString().Trim());
        }

        public static int? GetSafeInt(this SqlDataReader reader, string fieldName, bool isNullable = false)
        {
            if (!ContainValue(reader, fieldName, out object val))
                return isNullable ? (int?)null : 0;
            return int.Parse(val.ToString().Trim());
        }

        public static DateTime? GetSafeDateTime(this SqlDataReader reader, string fieldName)
        {
            if (!ContainValue(reader, fieldName, out object val))
                return (DateTime?)null;
            return DateTime.Parse(val.ToString().Trim());
        }

        private static bool ContainValue(SqlDataReader reader, string fieldName, out object val)
        {
            val = null;
            var exists = false;
            for (var i = 0; i < reader.FieldCount; ++i)
            {
                if (reader.GetName(i).Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
                return false;
            val = reader[fieldName];
            return val != null && val.ToString().Trim() != "";
        }
    }
}

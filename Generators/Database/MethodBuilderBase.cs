using System.Collections.Generic;
using System.Linq;
using T4Generators.Database.Schema;

namespace T4Generators.Database
{
    internal abstract class MethodBuilderBase
    {
        protected static string ColumnsToSqlParameters(IEnumerable<ColumnInfo> columns)
        {
            return string.Join(", ", columns.Select(c => "@" + c.Name));
        }

        protected static string ColumnsToDapperParameters(IEnumerable<ColumnInfo> columns)
        {
            return string.Join(", ", columns.Select(c => "item." + c.Name));
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using DataProvider.Schema;

namespace DataProvider
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
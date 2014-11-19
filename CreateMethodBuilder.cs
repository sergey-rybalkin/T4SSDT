using System.Linq;
using System.Text;
using DataProvider.Schema;

namespace DataProvider
{
    internal class CreateMethodBuilder : MethodBuilderBase, IMethodBuilder
    {
        private readonly TableInfo _table;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMethodBuilder" /> class.
        /// </summary>
        /// <param name="table">The table to build method for.</param>
        internal CreateMethodBuilder(TableInfo table)
        {
            _table = table;
        }

        /// <summary>
        /// Gets the method body without definition and curly braces.
        /// </summary>
        public string GetMethodBody()
        {
            StringBuilder buffer = new StringBuilder(512);

            buffer.AppendLine("using (var connection = GetRealTimeConnection())");
            buffer.AppendLine("{");
            buffer.AppendLine("connection.Open();");

            // Check whether we need to return a newly generated identity value.
            if (null != _table.IdentityColumn)
            {
                buffer.AppendFormat("{0} newId = connection.ExecuteScalar<{0}>(\"",
                                    _table.IdentityColumn.ClrType);
                buffer.AppendFormat("INSERT INTO {0} VALUES(", _table.FullTableName);

                var targetColumns = _table.Columns.Where(c => c != _table.IdentityColumn).ToArray();
                buffer.Append(ColumnsToSqlParameters(targetColumns));
                buffer.AppendLine(");SELECT SCOPE_IDENTITY()\", item);");
                buffer.AppendLine("return newId;");
            }
            else
            {
                buffer.Append("_connection.Execute(\"");
                buffer.AppendFormat("INSERT INTO {0} VALUES(", _table.FullTableName);

                buffer.Append(ColumnsToSqlParameters(_table.Columns));
                buffer.AppendLine(")\", item);");
            }

            buffer.AppendLine("}");

            return buffer.ToString();
        }
    }
}
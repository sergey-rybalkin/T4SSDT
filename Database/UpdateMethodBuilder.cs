using System.Collections.Generic;
using System.Linq;
using System.Text;
using T4Generators.Database.Schema;

namespace T4Generators.Database
{
    internal class UpdateMethodBuilder : MethodBuilderBase, IMethodBuilder
    {
        private readonly TableInfo _table;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMethodBuilder" /> class.
        /// </summary>
        /// <param name="table">The table to build method for.</param>
        internal UpdateMethodBuilder(TableInfo table)
        {
            _table = table;
        }

        /// <summary>
        /// Gets the method body without definition and curly braces.
        /// </summary>
        public string GetMethodBody()
        {
            StringBuilder buffer = new StringBuilder(512);
            var targetColumns = _table.Columns.Where(c => c != _table.IdentityColumn).ToArray();
            List<string> filter = new List<string>();

            if (_table.IdentityColumn != null)
                filter.Add(_table.IdentityColumn.Name);
            else if (null != _table.PrimaryKey)
            {
                foreach (var column in _table.PrimaryKey)
                    filter.Add(column.Name);
            }

            buffer.AppendLine("using (var connection = GetRealTimeConnection())");
            buffer.AppendLine("{");
            buffer.AppendLine("connection.Open();");

            buffer.AppendFormat("return connection.Execute(\"UPDATE {0} SET ", _table.FullTableName);
            buffer.Append(string.Join(", ", targetColumns.Select(c => string.Format("{0} = @{0}", c.Name))));
            buffer.Append(" WHERE ");
            buffer.Append(string.Join(" AND ", filter.Select(f => string.Format("{0} = @{0}", f))));
            buffer.AppendLine("\", item);");

            buffer.AppendLine("}");

            return buffer.ToString();
        }
    }
}
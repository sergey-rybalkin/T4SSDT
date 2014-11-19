using System.Linq;
using System.Text;
using DataProvider.Schema;

namespace DataProvider
{
    internal class DeleteMethodBuilder : MethodBuilderBase, IMethodBuilder
    {
        private readonly TableInfo _table;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteMethodBuilder" /> class.
        /// </summary>
        /// <param name="table">The table to build method for.</param>
        internal DeleteMethodBuilder(TableInfo table)
        {
            _table = table;
        }

        /// <summary>
        /// Gets the method body without definition and curly braces.
        /// </summary>
        public string GetMethodBody()
        {
            StringBuilder buffer = new StringBuilder(512);
            var signature = MethodSignature.GetDeleteSignature(_table);

            buffer.AppendLine("using (var connection = GetRealTimeConnection())");
            buffer.AppendLine("{");
            buffer.AppendLine("connection.Open();");

            buffer.AppendFormat("return connection.Execute(\"DELETE FROM {0} WHERE ", _table.FullTableName);
            buffer.Append(string.Join(" AND ",
                                      signature.Parameters.Select(p => string.Format("{0} = @{0}", p.Key))));
            buffer.Append("\", new { ");
            buffer.Append(string.Join(", ",
                                      signature.Parameters.Select(p => p.Key)));
            buffer.AppendLine(" });");

            buffer.AppendLine("}");

            return buffer.ToString();
        }
    }
}
using System.Linq;
using System.Text;
using DataProvider.Schema;

namespace DataProvider
{
    internal class ReadMethodBuilder : MethodBuilderBase, IMethodBuilder
    {
        private readonly TableInfo _table;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMethodBuilder" /> class.
        /// </summary>
        /// <param name="table">The table to build method for.</param>
        internal ReadMethodBuilder(TableInfo table)
        {
            _table = table;
        }

        /// <summary>
        /// Gets the method body without definition and curly braces.
        /// </summary>
        public string GetMethodBody()
        {
            StringBuilder buffer = new StringBuilder(512);
            var signature = MethodSignature.GetReadSignature(_table);
            var parameter = signature.Parameters.First();

            buffer.AppendLine("using (var connection = GetReadOnlyConnection())");
            buffer.AppendLine("{");
            buffer.AppendLine("connection.Open();");

            buffer.AppendFormat(
                "return connection.Query<{0}>(\"SELECT * FROM {1} WHERE {2} = @{3}\", new {{ {3} }})",
                _table.EntityName,
                _table.FullTableName,
                _table.IdentityColumn.FullName,
                parameter.Key);

            buffer.AppendLine(".SingleOrDefault();")
                  .AppendLine("}");

            return buffer.ToString();
        }
    }
}
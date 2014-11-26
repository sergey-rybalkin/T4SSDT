using System.Linq;
using System.Text;
using T4Generators.Database.Schema;

namespace T4Generators.Database
{
    internal class IndexMethodBuilder : MethodBuilderBase, IMethodBuilder
    {
        private readonly IndexInfo _index;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexMethodBuilder" /> class.
        /// </summary>
        /// <param name="index">The index to build method for.</param>
        internal IndexMethodBuilder(IndexInfo index)
        {
            _index = index;
        }

        /// <summary>
        /// Gets the method body without definition and curly braces.
        /// </summary>
        public string GetMethodBody()
        {
            StringBuilder buffer = new StringBuilder(512);

            var signature = MethodSignature.GetIndexSignature(_index);

            buffer.AppendLine("using (var connection = GetReadOnlyConnection())");
            buffer.AppendLine("{");
            buffer.AppendLine("connection.Open();");

            buffer.AppendFormat("return connection.Query<{0}>(\"SELECT * FROM {1} WHERE ",
                                _index.Table.EntityName,
                                _index.Table.FullTableName);
            buffer.Append(string.Join(" AND ", signature.Parameters.Select(p => p.Key + " = @" + p.Key)));
            buffer.Append("\", new { ");
            buffer.Append(string.Join(", ", signature.Parameters.Select(p => p.Key)));

            if (!_index.Unique)
                buffer.AppendLine(" });");
            else
                buffer.AppendLine(" }).FirstOrDefault();");

            buffer.AppendLine("}");

            return buffer.ToString();
        }
    }
}
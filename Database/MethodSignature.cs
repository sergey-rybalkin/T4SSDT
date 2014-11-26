using System;
using System.Collections.Generic;
using System.Linq;
using T4Generators.Database.Schema;

namespace T4Generators.Database
{
    /// <summary>
    /// Contains information required to create method definition.
    /// </summary>
    internal struct MethodSignature : IFormattable
    {
        internal MethodSignature(string name, string returnType, IDictionary<string, string> parameters)
            : this()
        {
            Name = name;
            ReturnType = returnType;
            Parameters = parameters;
        }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        internal string Name { get; private set; }

        /// <summary>
        /// Gets the common language runtime return value type as string.
        /// </summary>
        internal string ReturnType { get; private set; }

        /// <summary>
        /// Gets method parameters as name-type string pairs.
        /// </summary>
        internal IDictionary<string, string> Parameters { get; private set; }

        /// <summary>
        /// Formats the value of the current instance using the specified format. Pass "i" to get interface
        /// method declaration, otherwise public method definition will be returned.
        /// </summary>
        /// <param name="format">
        /// The format to use.-or- A null reference (Nothing in Visual Basic) to use the default format
        /// defined for the type of the <see cref="T:System.IFormattable" /> implementation.
        /// </param>
        /// <param name="formatProvider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual Basic) to obtain
        /// the numeric format information from the current locale setting of the operating system.
        /// </param>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (null == format || string.CompareOrdinal(format, "i") != 0)
                return string.Format("public {0} {1}({2})", ReturnType,  Name, ParametersToString());

            return string.Format("{0} {1}({2});", ReturnType, Name, ParametersToString());
        }

        /// <summary>
        /// Gets the create method signature for the specified table.
        /// </summary>
        /// <param name="table">The table to get signature for.</param>
        internal static MethodSignature GetCreateSignature(TableInfo table)
        {
            string returnType = table.IdentityColumn != null ? table.IdentityColumn.ClrType : "void";
            var parameters = new Dictionary<string, string> { { "item", table.EntityName } };

            return new MethodSignature("Create", returnType, parameters);
        }

        /// <summary>
        /// Gets the update method signature for the specified table.
        /// </summary>
        /// <param name="table">The table to get signature for.</param>
        internal static MethodSignature GetUpdateSignature(TableInfo table)
        {
            var parameters = new Dictionary<string, string> { { "item", table.EntityName } };

            return new MethodSignature("Update", "int", parameters);
        }

        /// <summary>
        /// Gets the delete method signature for the specified table.
        /// </summary>
        /// <param name="table">The table to get signature for.</param>
        internal static MethodSignature GetDeleteSignature(TableInfo table)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (table.IdentityColumn != null)
            {
                string name = ColumnNameToVariableName(table.IdentityColumn.Name);
                parameters.Add(name, table.IdentityColumn.ClrType);
            }
            else if (null != table.PrimaryKey)
            {
                foreach (var column in table.PrimaryKey)
                {
                    string name = ColumnNameToVariableName(column.Name);
                    parameters.Add(name, column.ClrType);
                }
            }

            return new MethodSignature("Delete" + table.EntityName, "int", parameters);
        }

        /// <summary>
        /// Gets the read method signature for the specified table.
        /// </summary>
        /// <param name="table">The table to get signature for.</param>
        internal static MethodSignature GetReadSignature(TableInfo table)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add(ColumnNameToVariableName(table.IdentityColumn.Name),
                           table.IdentityColumn.ClrType);

            return new MethodSignature("Get" + table.EntityName, table.EntityName, parameters);
        }

        /// <summary>
        /// Gets the signature of the read method for the specified index.
        /// </summary>
        /// <param name="index">The index to get signature for.</param>
        internal static MethodSignature GetIndexSignature(IndexInfo index)
        {
            string returnType = index.Unique
                ? index.Table.EntityName
                : string.Format("IEnumerable<{0}>", index.Table.EntityName);

            string name = string.Format("Get{0}",
                                        index.Unique ? index.Table.EntityName : index.Table.TableName);

            Dictionary<string, string> parameters = new Dictionary<string, string>(index.Columns.Length);
            foreach (var column in index.Columns)
            {
                string key = char.ToLowerInvariant(column.Name[0]) + column.Name.Substring(1);
                parameters.Add(key, column.ClrType);
            }

            return new MethodSignature(name, returnType, parameters);
        }

        /// <summary>
        /// Gets code string that represents method parameters definition.
        /// </summary>
        internal string ParametersToString()
        {
            var definitions = Parameters.Select(p => p.Value + ' ' + p.Key);

            return string.Join(", ", definitions);
        }

        private static string ColumnNameToVariableName(string name)
        {
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
    }
}
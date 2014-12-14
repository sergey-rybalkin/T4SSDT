using System.Collections.Generic;
using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.VisualStudio.TextTemplating;
using T4Generators.Database.Schema;

namespace T4Generators.Database
{
    /// <summary>
    /// Extends standard text transformation class with functionality specific to the database model.
    /// </summary>
    public abstract class DatabaseModelTransformation : TextTransformation
    {
        private DatabaseSchema _schema;

        /// <summary>
        /// Gets tables available in the database.
        /// </summary>
        public IEnumerable<TableInfo> Tables
        {
            get { return _schema.Tables; }
        }

        /// <summary>
        /// Gets views available in the database.
        /// </summary>
        public IEnumerable<ViewInfo> Views
        {
            get { return _schema.Views; }
        }

        public IEnumerable<IndexInfo> Indexes
        {
            get { return _schema.Indexes; }
        }

        /// <summary>
        /// Initializes host with database package on the specified path.
        /// </summary>
        /// <param name="databasePackagePath">Absolute path to the database package.</param>
        public void Initialize(string databasePackagePath)
        {
            var options = new ModelLoadOptions();
            options.LoadAsScriptBackedModel = true;
            options.ModelStorageType = DacSchemaModelStorageType.File;
            TSqlModel model = TSqlModel.LoadFromDacpac(databasePackagePath, options);

            using (model)
            {
                _schema = DatabaseSchema.FromModel(model);
            }
        }

        /// <summary>
        /// Writes the body of the POCO struct for the specified table.
        /// </summary>
        /// <param name="table">Target table.</param>
        /// <param name="excludedColumns">The excluded columns.</param>
        public void WritePocoDefinition(TableInfo table, HashSet<string> excludedColumns)
        {
            foreach (var column in table.Columns)
            {
                if (null != excludedColumns && excludedColumns.Contains(column.Name))
                    continue;

                WriteLine("public {0} {1} {{ get; set; }}", column.ClrType, column.Name);
            }
        }

        /// <summary>
        /// Writes CRUD interface methods definition for the specified table.
        /// </summary>
        /// <param name="table">The table to write interface for.</param>
        public void WriteTableDataProviderInterface(TableInfo table)
        {
            MethodSignature[] signatures = new MethodSignature[4];
            signatures[0] = MethodSignature.GetCreateSignature(table);
            signatures[1] = MethodSignature.GetUpdateSignature(table);
            signatures[2] = MethodSignature.GetDeleteSignature(table);
            signatures[3] = MethodSignature.GetReadSignature(table);

            foreach (var signature in signatures)
                WriteLine(signature.ToString("i", null));
        }

        /// <summary>
        /// Writes read interface methods definition for the specified index.
        /// </summary>
        /// <param name="index">The index to write interface for.</param>
        public void WriteIndexDataProviderInterface(IndexInfo index)
        {
            var signature = MethodSignature.GetIndexSignature(index);
            WriteLine(signature.ToString("i", null));
        }

        /// <summary>
        /// Writes data provider methods definition for the specified table.
        /// </summary>
        /// <param name="table">The table to write methods for.</param>
        public void WriteTableDataProviderImplementation(TableInfo table)
        {
            MethodSignature signature = MethodSignature.GetCreateSignature(table);
            IMethodBuilder builder = new CreateMethodBuilder(table);
            WrapMethodBody(signature, builder);

            signature = MethodSignature.GetUpdateSignature(table);
            builder = new UpdateMethodBuilder(table);
            WrapMethodBody(signature, builder);

            signature = MethodSignature.GetDeleteSignature(table);
            builder = new DeleteMethodBuilder(table);
            WrapMethodBody(signature, builder);

            signature = MethodSignature.GetReadSignature(table);
            builder = new ReadMethodBuilder(table);
            WrapMethodBody(signature, builder);
        }

        /// <summary>
        /// Writes read methods implementation for the specified index.
        /// </summary>
        /// <param name="index">The index to write methods for.</param>
        public void WriteIndexDataProviderImplementation(IndexInfo index)
        {
            var signature = MethodSignature.GetIndexSignature(index);
            IMethodBuilder builder = new IndexMethodBuilder(index);
            WrapMethodBody(signature, builder);
        }

        private void WrapMethodBody(MethodSignature signature, IMethodBuilder bodyWriter)
        {
            WriteLine(signature.ToString(null, null));
            WriteLine("{");
            PushIndent("    ");

            Write(bodyWriter.GetMethodBody());

            PopIndent();
            WriteLine("}");
        }
    }
}
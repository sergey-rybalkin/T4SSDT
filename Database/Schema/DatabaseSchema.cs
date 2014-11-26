using System;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using Microsoft.SqlServer.Dac.Model;

namespace T4Generators.Database.Schema
{
    internal class DatabaseSchema
    {
        private static readonly PluralizationService _pluralizationService =
            PluralizationService.CreateService(new CultureInfo("en"));

        private DatabaseSchema()
        {
        }

        internal TableInfo[] Tables { get; private set; }

        internal IndexInfo[] Indexes { get; private set; }

        internal static DatabaseSchema FromModel(TSqlModel model)
        {
            DatabaseSchema retVal = new DatabaseSchema();
            var tableModels = model.GetObjects(DacQueryScopes.All, ModelSchema.Table).ToArray();
            retVal.Tables = new TableInfo[tableModels.Count()];

            for (int i = 0; i < tableModels.Length; i++)
                retVal.Tables[i] = GetSchemaForTable(tableModels[i]);

            var keys = model.GetObjects(DacQueryScopes.All, ModelSchema.PrimaryKeyConstraint).ToArray();
            UpdatePrimaryKeysInformation(retVal.Tables, keys);

            var indexes = model.GetObjects(DacQueryScopes.All, ModelSchema.Index).ToArray();
            retVal.Indexes = new IndexInfo[indexes.Length];
            for (int i = 0; i < indexes.Length; i++)
            {
                var index = indexes[i];
                string tableName = index.Name.Parts[1];
                TableInfo targetTable = retVal.Tables.FirstOrDefault(t => t.TableName == tableName);
                if (null == targetTable)
                {
                    throw new InvalidOperationException(
                        "Could not find target table for index " + index.Name);
                }

                IndexInfo info = new IndexInfo();
                info.Table = targetTable;
                info.Unique = index.GetProperty<bool>(Index.Unique);

                var columns = index.GetReferenced(Index.Columns).ToArray();
                info.Columns = new ColumnInfo[columns.Length];
                for (int j = 0; j < info.Columns.Length; j++)
                {
                    info.Columns[j] = GetSchemaForColumn(columns[j]);
                }

                retVal.Indexes[i] = info;
            }

            return retVal;
        }

        private static void UpdatePrimaryKeysInformation(TableInfo[] tables, TSqlObject[] keys)
        {
            // Get schema for all primary keys
            foreach (var key in keys)
            {
                string keyName = key.Name.Parts[1];
                string tableName = keyName.Substring(3);
                var columns = key.GetReferenced(PrimaryKeyConstraint.Columns).ToArray();
                var primaryKeyColumns = new ColumnInfo[columns.Length];

                for (int i = 0; i < primaryKeyColumns.Length; i++)
                {
                    primaryKeyColumns[i] = GetSchemaForColumn(columns[i]);
                }

                var targetTable = tables.FirstOrDefault(t => t.TableName == tableName);
                if (null == targetTable)
                {
                    throw new InvalidOperationException(
                        "Could not find target table for primary key " + key.Name);
                }

                targetTable.PrimaryKey = primaryKeyColumns;
            }
        }

        private static TableInfo GetSchemaForTable(TSqlObject model)
        {
            TableInfo retVal = new TableInfo();
            retVal.EntityName = _pluralizationService.Singularize(model.Name.Parts[1]);
            retVal.TableName = model.Name.Parts[1];
            retVal.FullTableName = model.Name.ToString();

            var columns = model.GetReferenced(Table.Columns).ToArray();
            retVal.Columns = new ColumnInfo[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                ColumnInfo column = GetSchemaForColumn(columns[i]);
                retVal.Columns[i] = column;
                if (columns[i].GetProperty<bool>(Column.IsIdentity))
                    retVal.IdentityColumn = column;
            }

            return retVal;
        }

        private static ColumnInfo GetSchemaForColumn(TSqlObject model)
        {
            TSqlObject type = model.GetReferenced(Column.DataType).First();
            string dataType = type.Name.Parts[0];
            bool isNullable = model.GetProperty<bool>(Column.Nullable);
            int length = model.GetProperty<int>(Column.Length);

            return new ColumnInfo
            {
                Name = model.Name.Parts[2],
                FullName = model.Name.ToString(),
                SqlDataType = dataType,
                ClrType = GetTypeMapping(dataType, isNullable),
                Nullable = isNullable,
                Length = length
            };
        }

        private static string GetTypeMapping(string sqlTypeName, bool isNullable)
        {
            if (sqlTypeName.EndsWith("char"))
                return "string";

            string sysType = "string";
            switch (sqlTypeName)
            {
                case "bigint":
                    sysType = "long" + (isNullable ? "?" : string.Empty);
                    break;
                case "smallint":
                    sysType = "short" + (isNullable ? "?" : string.Empty);
                    break;
                case "int":
                    sysType = "int" + (isNullable ? "?" : string.Empty);
                    break;
                case "uniqueidentifier":
                    sysType = "Guid" + (isNullable ? "?" : string.Empty);
                    break;
                case "smalldatetime":
                case "datetime":
                case "datetime2":
                case "date":
                    sysType = "DateTime" + (isNullable ? "?" : string.Empty);
                    break;
                case "time":
                    sysType = "TimeSpan" + (isNullable ? "?" : string.Empty);
                    break;
                case "float":
                    sysType = "double" + (isNullable ? "?" : string.Empty);
                    break;
                case "real":
                    sysType = "float" + (isNullable ? "?" : string.Empty);
                    break;
                case "numeric":
                case "smallmoney":
                case "decimal":
                case "money":
                    sysType = "decimal" + (isNullable ? "?" : string.Empty);
                    break;
                case "tinyint":
                    sysType = "byte" + (isNullable ? "?" : string.Empty);
                    break;
                case "bit":
                    sysType = "bool" + (isNullable ? "?" : string.Empty);
                    break;
                case "image":
                case "binary":
                case "varbinary":
                case "timestamp":
                    sysType = "byte[]";
                    break;
                case "geography":
                    sysType = "Microsoft.SqlServer.Types.SqlGeography" + (isNullable ? "?" : string.Empty);
                    break;
                case "geometry":
                    sysType = "Microsoft.SqlServer.Types.SqlGeometry" + (isNullable ? "?" : string.Empty);
                    break;
            }

            return sysType;
        }
    }
}
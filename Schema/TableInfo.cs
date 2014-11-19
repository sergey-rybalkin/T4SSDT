namespace DataProvider.Schema
{
    /// <summary>
    /// Contains database table information.
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the fully qualified name of the table (including schema name etc.).
        /// </summary>
        public string FullTableName { get; set; }

        /// <summary>
        /// Gets or sets the name of the entity that this table represents. Usually is a singular form of the
        /// table name.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the identity column information if any.
        /// </summary>
        public ColumnInfo IdentityColumn { get; set; }

        /// <summary>
        /// Gets or sets the primary key columns information if any.
        /// </summary>
        public ColumnInfo[] PrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets the collection of columns this table contains.
        /// </summary>
        public ColumnInfo[] Columns { get; set; }
    }
}
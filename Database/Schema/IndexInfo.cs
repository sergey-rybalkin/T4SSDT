namespace T4Generators.Database.Schema
{
    /// <summary>
    /// Contains database index information.
    /// </summary>
    public class IndexInfo
    {
        /// <summary>
        /// Gets or sets the table this index belongs to.
        /// </summary>
        public TableInfo Table { get; set; }

        /// <summary>
        /// Gets or sets indexed columns.
        /// </summary>
        public ColumnInfo[] Columns { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this index contains unique values.
        /// </summary>
        public bool Unique { get; set; }
    }
}
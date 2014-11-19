namespace DataProvider.Schema
{
    /// <summary>
    /// Contains database column information.
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the fully qualified name of the column.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets SQL data type of the column.
        /// </summary>
        public string SqlDataType { get; set; }

        /// <summary>
        /// Gets or sets Clr data type of the column.
        /// </summary>
        public string ClrType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ColumnInfo"/> is nullable.
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the data stored in this column.
        /// </summary>
        public int Length { get; set; }
    }
}
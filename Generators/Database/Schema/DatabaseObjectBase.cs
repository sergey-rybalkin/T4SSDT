namespace T4Generators.Database.Schema
{
    /// <summary>
    /// Contains common information for all database objects.
    /// </summary>
    public abstract class DatabaseObjectBase
    {
        /// <summary>
        /// Gets or sets the name of the database object without schema name.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the fully qualified name of the object including schema and optional parent object
        /// name.
        /// </summary>
        public string FullName { get; set; }
    }
}
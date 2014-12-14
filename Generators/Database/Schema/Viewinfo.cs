using System.Dynamic;

namespace T4Generators.Database.Schema
{
    /// <summary>
    /// Contains database view information.
    /// </summary>
    public class ViewInfo : DatabaseObjectBase
    {
        /// <summary>
        /// Gets or sets the collection of columns returned from this view.
        /// </summary>
        public ColumnInfo[] Columns { get; set; }
    }
}
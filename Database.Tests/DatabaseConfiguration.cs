namespace Common.Configuration
{
    public struct DatabaseConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConfiguration" /> struct.
        /// </summary>
        /// <param name="realTimeConnectionString">The real time connection string.</param>
        /// <param name="readOnlyConnectionString">The read only connection string.</param>
        /// <param name="archiveConnectionString">The archive connection string.</param>
        public DatabaseConfiguration(string realTimeConnectionString,
                                     string readOnlyConnectionString,
                                     string archiveConnectionString)
            : this()
        {
            RealTimeConnectionString = realTimeConnectionString;
            ReadOnlyConnectionString = readOnlyConnectionString;
            ArchiveConnectionString = archiveConnectionString;
        }

        /// <summary>
        /// Gets the master database connection string that provides read/write access to the actual data.
        /// </summary>
        public string RealTimeConnectionString { get; private set; }

        /// <summary>
        /// Gets the read only database connection string.
        /// </summary>
        public string ReadOnlyConnectionString { get; private set; }

        /// <summary>
        /// Gets the archive database connection string that contains read-only data snapshot that might be
        /// outdated.
        /// </summary>
        public string ArchiveConnectionString { get; private set; }
    }
}
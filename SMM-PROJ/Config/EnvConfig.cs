using DotNetEnv;

namespace SMM_PROJ.Config
{
    /// <summary>
    /// Reads environment variables from .env file and exposes the database connection string.
    /// Must be initialised once at application startup via <see cref="Load"/>.
    /// </summary>
    public static class EnvConfig
    {
        /// <summary>
        /// Gets the SQL Server connection string built from .env variables.
        /// </summary>
        public static string ConnectionString { get; private set; } = string.Empty;

        /// <summary>
        /// Loads the .env file from the application base directory and builds the connection string.
        /// If <c>CONNECTION_STRING</c> is set, it is used (and <c>Initial Catalog</c> is appended when missing).
        /// Otherwise the string is built from <c>DB_SERVER</c>, <c>DB_NAME</c>, and related options.
        /// </summary>
        public static void Load()
        {
            Env.Load();

            string database = Env.GetString("DB_NAME", "SocietiesManagementSystem");
            string fullFromEnv = Env.GetString("CONNECTION_STRING", string.Empty).Trim();

            if (!string.IsNullOrEmpty(fullFromEnv))
            {
                ConnectionString = EnsureDatabaseInConnectionString(fullFromEnv, database);
                return;
            }

            string server = Env.GetString("DB_SERVER", @"localhost\SQLEXPRESS01");
            string intSec = Env.GetString("DB_INTEGRATED_SECURITY", "True");

            // Matches typical SSMS / SqlClient defaults for local SQL Express (Encrypt + TrustServerCertificate).
            ConnectionString =
                $"Data Source={server};Initial Catalog={database};Integrated Security={intSec};" +
                "Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;" +
                "Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;Packet Size=4096;Command Timeout=0;";
        }

        /// <summary>
        /// Ensures the connection string includes a database/catalog when only server settings were provided.
        /// </summary>
        private static string EnsureDatabaseInConnectionString(string connectionString, string database)
        {
            bool hasCatalog =
                connectionString.Contains("Initial Catalog=", StringComparison.OrdinalIgnoreCase) ||
                connectionString.Contains("Database=", StringComparison.OrdinalIgnoreCase);

            if (hasCatalog)
                return connectionString;

            char sep = connectionString.EndsWith(';') ? ' ' : ';';
            return $"{connectionString}{sep}Initial Catalog={database};";
        }
    }
}

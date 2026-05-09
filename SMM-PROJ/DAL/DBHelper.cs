using System.Data;
using Microsoft.Data.SqlClient;
using SMM_PROJ.Config;

namespace SMM_PROJ.DAL
{
    /// <summary>
    /// Centralised database helper. All database operations flow through this class.
    /// Connection string is read once from <see cref="EnvConfig"/>.
    /// </summary>
    public static class DBHelper
    {
        /// <summary>
        /// Creates and returns an open <see cref="SqlConnection"/>.
        /// Caller is responsible for disposing.
        /// </summary>
        public static SqlConnection GetConnection()
        {
            var connection = new SqlConnection(EnvConfig.ConnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Executes a non-query command (INSERT, UPDATE, DELETE) and returns the number of rows affected.
        /// </summary>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using var connection = GetConnection();
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes a SELECT query and returns the result as a <see cref="DataTable"/>.
        /// </summary>
        public static DataTable ExecuteReader(string query, params SqlParameter[] parameters)
        {
            using var connection = GetConnection();
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parameters);

            using var adapter = new SqlDataAdapter(command);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        /// <summary>
        /// Executes a query that returns a single scalar value (e.g. COUNT, MAX).
        /// </summary>
        public static object? ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using var connection = GetConnection();
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parameters);
            return command.ExecuteScalar();
        }
    }
}

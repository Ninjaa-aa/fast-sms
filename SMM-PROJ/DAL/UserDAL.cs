using System.Data;
using Microsoft.Data.SqlClient;
using SMM_PROJ.Models;

namespace SMM_PROJ.DAL
{
    /// <summary>
    /// Data access layer for the Users table.
    /// </summary>
    public static class UserDAL
    {
        /// <summary>
        /// Returns a <see cref="User"/> matching the given email, or null if not found.
        /// </summary>
        public static User? GetByEmail(string email)
        {
            const string query = "SELECT * FROM Users WHERE Email = @Email";
            var table = DBHelper.ExecuteReader(query, new SqlParameter("@Email", email));

            if (table.Rows.Count == 0) return null;

            DataRow row = table.Rows[0];
            return MapUser(row);
        }

        /// <summary>
        /// Returns true if the email is already registered.
        /// </summary>
        public static bool EmailExists(string email)
        {
            const string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
            var result = DBHelper.ExecuteScalar(query, new SqlParameter("@Email", email));
            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// Inserts a new user. The <see cref="User.PasswordHash"/> must already be hashed.
        /// Returns true on success.
        /// </summary>
        public static bool Register(User user)
        {
            const string query =
                @"INSERT INTO Users (FullName, Email, PasswordHash, Role)
                  VALUES (@FullName, @Email, @PasswordHash, @Role)";

            int rows = DBHelper.ExecuteNonQuery(query,
                new SqlParameter("@FullName", user.FullName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@Role", user.Role));

            return rows > 0;
        }

        /// <summary>
        /// Returns all non-Admin users as a <see cref="DataTable"/> for the admin grid.
        /// </summary>
        public static DataTable GetAll()
        {
            const string query =
                "SELECT UserID, FullName, Email, Role, CreatedAt FROM Users WHERE Role != 'Admin'";
            return DBHelper.ExecuteReader(query);
        }

        /// <summary>
        /// Searches non-Admin users by name or email (LIKE match).
        /// </summary>
        public static DataTable Search(string searchTerm)
        {
            const string query =
                @"SELECT UserID, FullName, Email, Role, CreatedAt
                  FROM Users
                  WHERE Role != 'Admin'
                    AND (FullName LIKE @Term OR Email LIKE @Term)";

            return DBHelper.ExecuteReader(query,
                new SqlParameter("@Term", $"%{searchTerm}%"));
        }

        /// <summary>
        /// Deletes a user by ID. Returns true if a row was removed.
        /// </summary>
        public static bool Delete(int userId)
        {
            const string query = "DELETE FROM Users WHERE UserID = @UserID";
            int rows = DBHelper.ExecuteNonQuery(query, new SqlParameter("@UserID", userId));
            return rows > 0;
        }

        /// <summary>
        /// Returns the total count of non-Admin users.
        /// </summary>
        public static int GetCount()
        {
            const string query = "SELECT COUNT(*) FROM Users WHERE Role != 'Admin'";
            return Convert.ToInt32(DBHelper.ExecuteScalar(query));
        }

        /// <summary>
        /// Returns all users with the SocietyHead role as a <see cref="DataTable"/>.
        /// Used when assigning a head to a new society.
        /// </summary>
        public static DataTable GetSocietyHeads()
        {
            const string query =
                "SELECT UserID, FullName, Email FROM Users WHERE Role = 'SocietyHead'";
            return DBHelper.ExecuteReader(query);
        }

        private static User MapUser(DataRow row)
        {
            return new User
            {
                UserID = Convert.ToInt32(row["UserID"]),
                FullName = row["FullName"].ToString()!,
                Email = row["Email"].ToString()!,
                PasswordHash = row["PasswordHash"].ToString()!,
                Role = row["Role"].ToString()!,
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            };
        }
    }
}

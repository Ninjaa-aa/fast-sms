using System.Data;
using Microsoft.Data.SqlClient;

namespace SMM_PROJ.DAL
{
    /// <summary>
    /// Data access layer for the Societies table.
    /// </summary>
    public static class SocietyDAL
    {
        /// <summary>
        /// Returns all societies with Status = 'Active'.
        /// </summary>
        public static DataTable GetActive()
        {
            const string query =
                "SELECT SocietyID, Name, Description, Status FROM Societies WHERE Status = 'Active'";
            return DBHelper.ExecuteReader(query);
        }

        /// <summary>
        /// Returns all societies with the head user's name joined in.
        /// </summary>
        public static DataTable GetAll()
        {
            const string query =
                @"SELECT s.SocietyID, s.Name, u.FullName AS Head, s.Status, s.CreatedAt
                  FROM Societies s
                  JOIN Users u ON s.HeadUserID = u.UserID";
            return DBHelper.ExecuteReader(query);
        }

        /// <summary>
        /// Returns the society managed by the given head user, or an empty table if none.
        /// </summary>
        public static DataTable GetByHead(int headUserId)
        {
            const string query =
                "SELECT SocietyID, Name, Status FROM Societies WHERE HeadUserID = @HeadUserID";
            return DBHelper.ExecuteReader(query, new SqlParameter("@HeadUserID", headUserId));
        }

        /// <summary>
        /// Returns the count of active societies.
        /// </summary>
        public static int GetActiveCount()
        {
            const string query = "SELECT COUNT(*) FROM Societies WHERE Status = 'Active'";
            return Convert.ToInt32(DBHelper.ExecuteScalar(query));
        }

        /// <summary>
        /// Creates a new society with Pending status.
        /// </summary>
        public static bool Create(string name, string description, int headUserId)
        {
            const string query =
                @"INSERT INTO Societies (Name, Description, HeadUserID, Status)
                  VALUES (@Name, @Description, @HeadUserID, 'Pending')";

            int rows = DBHelper.ExecuteNonQuery(query,
                new SqlParameter("@Name", name),
                new SqlParameter("@Description", description),
                new SqlParameter("@HeadUserID", headUserId));

            return rows > 0;
        }

        /// <summary>
        /// Sets a society's status to 'Active'.
        /// </summary>
        public static bool Approve(int societyId)
        {
            const string query = "UPDATE Societies SET Status = 'Active' WHERE SocietyID = @SocietyID";
            return DBHelper.ExecuteNonQuery(query, new SqlParameter("@SocietyID", societyId)) > 0;
        }

        /// <summary>
        /// Sets a society's status to 'Suspended'.
        /// </summary>
        public static bool Suspend(int societyId)
        {
            const string query = "UPDATE Societies SET Status = 'Suspended' WHERE SocietyID = @SocietyID";
            return DBHelper.ExecuteNonQuery(query, new SqlParameter("@SocietyID", societyId)) > 0;
        }

        /// <summary>
        /// Deletes a society by ID.
        /// </summary>
        public static bool Delete(int societyId)
        {
            const string query = "DELETE FROM Societies WHERE SocietyID = @SocietyID";
            return DBHelper.ExecuteNonQuery(query, new SqlParameter("@SocietyID", societyId)) > 0;
        }

        /// <summary>
        /// Returns a performance summary: society name, member count, event count.
        /// </summary>
        public static DataTable GetPerformanceSummary()
        {
            const string query =
                @"SELECT s.Name,
                         COUNT(DISTINCT CASE WHEN m.Status = 'Approved' THEN m.UserID END) AS Members,
                         COUNT(DISTINCT CASE WHEN e.Status = 'Approved' THEN e.EventID END) AS Events
                  FROM Societies s
                  LEFT JOIN Memberships m ON s.SocietyID = m.SocietyID
                  LEFT JOIN Events e ON s.SocietyID = e.SocietyID
                  GROUP BY s.Name";
            return DBHelper.ExecuteReader(query);
        }
    }
}

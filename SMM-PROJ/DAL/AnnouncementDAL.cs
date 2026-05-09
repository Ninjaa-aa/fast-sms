using System.Data;
using Microsoft.Data.SqlClient;

namespace SMM_PROJ.DAL
{
    /// <summary>
    /// Data access layer for the Announcements table.
    /// </summary>
    public static class AnnouncementDAL
    {
        /// <summary>
        /// Returns all announcements for a given society.
        /// </summary>
        public static DataTable GetBySociety(int societyId)
        {
            const string query =
                "SELECT AnnouncementID, Title, Content, PostedAt FROM Announcements WHERE SocietyID = @SID";
            return DBHelper.ExecuteReader(query, new SqlParameter("@SID", societyId));
        }

        /// <summary>
        /// Creates a new announcement for a society.
        /// </summary>
        public static bool Create(int societyId, string title, string content)
        {
            const string query =
                @"INSERT INTO Announcements (SocietyID, Title, Content)
                  VALUES (@SID, @Title, @Content)";

            int rows = DBHelper.ExecuteNonQuery(query,
                new SqlParameter("@SID", societyId),
                new SqlParameter("@Title", title),
                new SqlParameter("@Content", content));

            return rows > 0;
        }
    }
}

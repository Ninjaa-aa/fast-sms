using System.Data;
using Microsoft.Data.SqlClient;

namespace SMM_PROJ.DAL
{
    /// <summary>
    /// Data access layer for the Memberships table.
    /// </summary>
    public static class MembershipDAL
    {
        /// <summary>
        /// Returns true if the user has already applied (Pending or Approved) for the given society.
        /// </summary>
        public static bool HasApplied(int userId, int societyId)
        {
            const string query =
                @"SELECT COUNT(*) FROM Memberships
                  WHERE UserID = @UserID AND SocietyID = @SocietyID AND Status != 'Rejected'";

            var result = DBHelper.ExecuteScalar(query,
                new SqlParameter("@UserID", userId),
                new SqlParameter("@SocietyID", societyId));

            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// Inserts a new Pending membership application.
        /// </summary>
        public static bool Apply(int userId, int societyId)
        {
            const string query =
                @"INSERT INTO Memberships (UserID, SocietyID, Status)
                  VALUES (@UserID, @SocietyID, 'Pending')";

            int rows = DBHelper.ExecuteNonQuery(query,
                new SqlParameter("@UserID", userId),
                new SqlParameter("@SocietyID", societyId));

            return rows > 0;
        }

        /// <summary>
        /// Returns all memberships for a given user (joined with society name).
        /// </summary>
        public static DataTable GetByUser(int userId)
        {
            const string query =
                @"SELECT s.Name AS Society, m.Status, m.AppliedAt
                  FROM Memberships m
                  JOIN Societies s ON m.SocietyID = s.SocietyID
                  WHERE m.UserID = @UserID";

            return DBHelper.ExecuteReader(query, new SqlParameter("@UserID", userId));
        }

        /// <summary>
        /// Returns all memberships for a given society (joined with user info).
        /// </summary>
        public static DataTable GetBySociety(int societyId)
        {
            const string query =
                @"SELECT m.MembershipID, u.FullName, u.Email, m.Status, m.AppliedAt
                  FROM Memberships m
                  JOIN Users u ON m.UserID = u.UserID
                  WHERE m.SocietyID = @SocietyID";

            return DBHelper.ExecuteReader(query, new SqlParameter("@SocietyID", societyId));
        }

        /// <summary>
        /// Returns all memberships for a given society filtered by status.
        /// </summary>
        public static DataTable GetBySocietyFiltered(int societyId, string status)
        {
            const string query =
                @"SELECT m.MembershipID, u.FullName, u.Email, m.Status, m.AppliedAt
                  FROM Memberships m
                  JOIN Users u ON m.UserID = u.UserID
                  WHERE m.SocietyID = @SocietyID AND m.Status = @Status";

            return DBHelper.ExecuteReader(query,
                new SqlParameter("@SocietyID", societyId),
                new SqlParameter("@Status", status));
        }

        /// <summary>
        /// Approves a membership request.
        /// </summary>
        public static bool Approve(int membershipId)
        {
            const string query =
                "UPDATE Memberships SET Status = 'Approved', ApprovedAt = GETDATE() WHERE MembershipID = @MID";
            return DBHelper.ExecuteNonQuery(query, new SqlParameter("@MID", membershipId)) > 0;
        }

        /// <summary>
        /// Rejects a membership request.
        /// </summary>
        public static bool Reject(int membershipId)
        {
            const string query =
                "UPDATE Memberships SET Status = 'Rejected' WHERE MembershipID = @MID";
            return DBHelper.ExecuteNonQuery(query, new SqlParameter("@MID", membershipId)) > 0;
        }

        /// <summary>
        /// Returns approved members of a society (UserID + FullName) for dropdowns.
        /// </summary>
        public static DataTable GetApprovedMembers(int societyId)
        {
            const string query =
                @"SELECT u.UserID, u.FullName
                  FROM Memberships m
                  JOIN Users u ON m.UserID = u.UserID
                  WHERE m.SocietyID = @SocietyID AND m.Status = 'Approved'";

            return DBHelper.ExecuteReader(query, new SqlParameter("@SocietyID", societyId));
        }

        /// <summary>
        /// Returns all memberships across all societies (for admin reports).
        /// </summary>
        public static DataTable GetAll()
        {
            const string query =
                @"SELECT u.FullName, u.Email, s.Name AS Society, m.Status
                  FROM Memberships m
                  JOIN Users u ON m.UserID = u.UserID
                  JOIN Societies s ON m.SocietyID = s.SocietyID";
            return DBHelper.ExecuteReader(query);
        }
    }
}

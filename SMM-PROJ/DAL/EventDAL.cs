using System.Data;
using Microsoft.Data.SqlClient;

namespace SMM_PROJ.DAL
{
    /// <summary>
    /// Data access layer for the Events and EventRegistrations tables.
    /// </summary>
    public static class EventDAL
    {
        /// <summary>
        /// Returns all upcoming approved events (future date only).
        /// </summary>
        public static DataTable GetUpcomingApproved()
        {
            const string query =
                @"SELECT e.EventID, e.Title, s.Name AS Society, e.EventDate, e.Venue
                  FROM Events e
                  JOIN Societies s ON e.SocietyID = s.SocietyID
                  WHERE e.Status = 'Approved' AND e.EventDate >= GETDATE()";
            return DBHelper.ExecuteReader(query);
        }

        /// <summary>
        /// Returns true if the user is already registered for the given event.
        /// </summary>
        public static bool IsRegistered(int eventId, int userId)
        {
            const string query =
                "SELECT COUNT(*) FROM EventRegistrations WHERE EventID = @EventID AND UserID = @UserID";
            var result = DBHelper.ExecuteScalar(query,
                new SqlParameter("@EventID", eventId),
                new SqlParameter("@UserID", userId));
            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// Registers a user for an event and generates a ticket code.
        /// </summary>
        public static bool Register(int eventId, int userId)
        {
            string ticketCode = "TKT-" + Guid.NewGuid().ToString("N")[..8].ToUpper();

            const string query =
                @"INSERT INTO EventRegistrations (EventID, UserID, TicketCode)
                  VALUES (@EventID, @UserID, @TicketCode)";

            int rows = DBHelper.ExecuteNonQuery(query,
                new SqlParameter("@EventID", eventId),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@TicketCode", ticketCode));

            return rows > 0;
        }

        /// <summary>
        /// Returns all event tickets/passes for a given user.
        /// </summary>
        public static DataTable GetTicketsByUser(int userId)
        {
            const string query =
                @"SELECT e.Title, s.Name AS Society, e.EventDate, e.Venue,
                         er.TicketCode, er.RegisteredAt
                  FROM EventRegistrations er
                  JOIN Events e ON er.EventID = e.EventID
                  JOIN Societies s ON e.SocietyID = s.SocietyID
                  WHERE er.UserID = @UserID";

            return DBHelper.ExecuteReader(query, new SqlParameter("@UserID", userId));
        }

        /// <summary>
        /// Returns all events for a specific society.
        /// </summary>
        public static DataTable GetBySociety(int societyId)
        {
            const string query =
                "SELECT EventID, Title, Description, EventDate, Venue, Status FROM Events WHERE SocietyID = @SID";
            return DBHelper.ExecuteReader(query, new SqlParameter("@SID", societyId));
        }

        /// <summary>
        /// Creates a new event with Pending status (requires admin approval).
        /// </summary>
        public static bool Create(int societyId, string title, string description, DateTime eventDate, string venue)
        {
            const string query =
                @"INSERT INTO Events (SocietyID, Title, Description, EventDate, Venue, Status)
                  VALUES (@SID, @Title, @Description, @EventDate, @Venue, 'Pending')";

            int rows = DBHelper.ExecuteNonQuery(query,
                new SqlParameter("@SID", societyId),
                new SqlParameter("@Title", title),
                new SqlParameter("@Description", description),
                new SqlParameter("@EventDate", eventDate),
                new SqlParameter("@Venue", venue));

            return rows > 0;
        }

        /// <summary>
        /// Cancels an event.
        /// </summary>
        public static bool Cancel(int eventId)
        {
            const string query = "UPDATE Events SET Status = 'Cancelled' WHERE EventID = @EID";
            return DBHelper.ExecuteNonQuery(query, new SqlParameter("@EID", eventId)) > 0;
        }

        /// <summary>
        /// Returns all pending events (for admin approval view).
        /// </summary>
        public static DataTable GetPending()
        {
            const string query =
                @"SELECT e.EventID, e.Title, s.Name AS Society, e.EventDate, e.Venue, e.Status
                  FROM Events e
                  JOIN Societies s ON e.SocietyID = s.SocietyID
                  WHERE e.Status = 'Pending'";
            return DBHelper.ExecuteReader(query);
        }

        /// <summary>
        /// Approves an event.
        /// </summary>
        public static bool Approve(int eventId)
        {
            const string query = "UPDATE Events SET Status = 'Approved' WHERE EventID = @EID";
            return DBHelper.ExecuteNonQuery(query, new SqlParameter("@EID", eventId)) > 0;
        }

        /// <summary>
        /// Rejects an event (sets status to Cancelled).
        /// </summary>
        public static bool Reject(int eventId)
        {
            const string query = "UPDATE Events SET Status = 'Cancelled' WHERE EventID = @EID";
            return DBHelper.ExecuteNonQuery(query, new SqlParameter("@EID", eventId)) > 0;
        }

        /// <summary>
        /// Returns the count of pending events.
        /// </summary>
        public static int GetPendingCount()
        {
            const string query = "SELECT COUNT(*) FROM Events WHERE Status = 'Pending'";
            return Convert.ToInt32(DBHelper.ExecuteScalar(query));
        }

        /// <summary>
        /// Returns all events across all societies (for admin reports).
        /// </summary>
        public static DataTable GetAll()
        {
            const string query =
                @"SELECT e.Title, s.Name AS Society, e.EventDate, e.Venue, e.Status
                  FROM Events e
                  JOIN Societies s ON e.SocietyID = s.SocietyID";
            return DBHelper.ExecuteReader(query);
        }
    }
}

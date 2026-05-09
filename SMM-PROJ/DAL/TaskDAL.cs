using System.Data;
using Microsoft.Data.SqlClient;

namespace SMM_PROJ.DAL
{
    /// <summary>
    /// Data access layer for the Tasks table.
    /// </summary>
    public static class TaskDAL
    {
        /// <summary>
        /// Returns all tasks for a given society, with the assigned user's name.
        /// </summary>
        public static DataTable GetBySociety(int societyId)
        {
            const string query =
                @"SELECT t.TaskID, t.Title, u.FullName AS AssignedTo, t.DueDate, t.Status
                  FROM Tasks t
                  JOIN Users u ON t.AssignedTo = u.UserID
                  WHERE t.SocietyID = @SID";

            return DBHelper.ExecuteReader(query, new SqlParameter("@SID", societyId));
        }

        /// <summary>
        /// Creates a new task assigned to a society member.
        /// </summary>
        public static bool Create(int societyId, int assignedTo, int assignedBy,
            string title, string description, DateTime dueDate)
        {
            const string query =
                @"INSERT INTO Tasks (SocietyID, AssignedTo, AssignedBy, Title, Description, DueDate, Status)
                  VALUES (@SID, @AssignedTo, @AssignedBy, @Title, @Description, @DueDate, 'Pending')";

            int rows = DBHelper.ExecuteNonQuery(query,
                new SqlParameter("@SID", societyId),
                new SqlParameter("@AssignedTo", assignedTo),
                new SqlParameter("@AssignedBy", assignedBy),
                new SqlParameter("@Title", title),
                new SqlParameter("@Description", description),
                new SqlParameter("@DueDate", dueDate));

            return rows > 0;
        }

        /// <summary>
        /// Marks a task as completed.
        /// </summary>
        public static bool MarkComplete(int taskId)
        {
            const string query = "UPDATE Tasks SET Status = 'Completed' WHERE TaskID = @TID";
            return DBHelper.ExecuteNonQuery(query, new SqlParameter("@TID", taskId)) > 0;
        }
    }
}

namespace SMM_PROJ.Helpers
{
    /// <summary>
    /// Holds the currently logged-in user's information for the lifetime of the session.
    /// Call <see cref="Clear"/> on logout.
    /// </summary>
    public static class Session
    {
        public static int UserID { get; set; }
        public static string FullName { get; set; } = string.Empty;
        public static string Email { get; set; } = string.Empty;
        public static string Role { get; set; } = string.Empty;

        /// <summary>
        /// Society ID for the SocietyHead role. Null for other roles.
        /// </summary>
        public static int? SocietyID { get; set; }

        /// <summary>
        /// Resets all session properties to their defaults.
        /// </summary>
        public static void Clear()
        {
            UserID = 0;
            FullName = string.Empty;
            Email = string.Empty;
            Role = string.Empty;
            SocietyID = null;
        }
    }
}

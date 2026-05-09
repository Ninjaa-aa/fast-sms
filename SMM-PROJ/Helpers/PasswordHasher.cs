namespace SMM_PROJ.Helpers
{
    /// <summary>
    /// Wraps BCrypt for password hashing and verification.
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Returns a BCrypt hash of the given plain-text password.
        /// </summary>
        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifies a plain-text password against a BCrypt hash.
        /// </summary>
        public static bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}

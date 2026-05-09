namespace SMM_PROJ.Models
{
    /// <summary>
    /// Represents a row in the Memberships table.
    /// </summary>
    public class Membership
    {
        public int MembershipID { get; set; }
        public int UserID { get; set; }
        public int SocietyID { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime AppliedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}

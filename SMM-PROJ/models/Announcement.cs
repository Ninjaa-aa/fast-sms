namespace SMM_PROJ.Models
{
    /// <summary>
    /// Represents a row in the Announcements table.
    /// </summary>
    public class Announcement
    {
        public int AnnouncementID { get; set; }
        public int SocietyID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PostedAt { get; set; }
    }
}

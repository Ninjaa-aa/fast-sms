namespace SMM_PROJ.Models
{
    /// <summary>
    /// Represents a row in the Events table.
    /// </summary>
    public class Event
    {
        public int EventID { get; set; }
        public int SocietyID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}

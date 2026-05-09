namespace SMM_PROJ.Models
{
    /// <summary>
    /// Represents a row in the Societies table.
    /// </summary>
    public class Society
    {
        public int SocietyID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int HeadUserID { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}

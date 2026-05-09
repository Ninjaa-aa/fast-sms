namespace SMM_PROJ.Models
{
    /// <summary>
    /// Represents a row in the Tasks table.
    /// Named TaskItem to avoid collision with <see cref="System.Threading.Tasks.Task"/>.
    /// </summary>
    public class TaskItem
    {
        public int TaskID { get; set; }
        public int SocietyID { get; set; }
        public int AssignedTo { get; set; }
        public int AssignedBy { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}

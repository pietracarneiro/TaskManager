namespace TaskManager.DTOs.Requests
{
    public class TaskItemRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }
    }
}

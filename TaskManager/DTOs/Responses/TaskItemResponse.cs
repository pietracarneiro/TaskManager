namespace TaskManager.DTOs.Responses
{
    public class TaskItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}

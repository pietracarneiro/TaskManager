namespace TaskManager.DTOs.Requests
{
    public class TaskItemRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        /// <summary>  
        /// Data de vencimento da tarefa. Deve ser informada no formato ISO 8601: "yyyy-MM-ddTHH:mm:ss".  
        /// Exemplo: "2023-10-15T14:30:00".  
        /// </summary>  
        public DateTime? DueDate { get; set; }

        /// <summary>  
        /// Status da tarefa. Valores possíveis: "Pendente", "Em progresso", "Concluído".  
        /// </summary>  
        public string? Status { get; set; }
    }
}

using TaskManager.Models;

namespace TaskManager.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<TaskItem> Create(TaskItem taskItem);
        Task<TaskItem> Update(int taskId, TaskItem updatedTaskItem);
        Task<TaskItem> Delete(int taskId);
        Task<List<TaskItem>> GetAll();
        Task<List<TaskItem>> GetAllByStatus(string status);
        Task<List<TaskItem>> GetAllByDueDate(DateTime date);
    }
}

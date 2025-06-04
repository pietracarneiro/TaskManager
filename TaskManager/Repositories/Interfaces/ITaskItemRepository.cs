using TaskManager.Models;

namespace TaskManager.Repositories.Interfaces
{
    public interface ITaskItemRepository
    {
        public Task<TaskItem> Create(TaskItem task);
        public Task<TaskItem> Update(int taskId, TaskItem updatedTaskItem);
        public Task<TaskItem> Delete(int taskId);
        public Task<List<TaskItem>> GetAll();
        public Task<List<TaskItem>> GetAllByStatus(string status);
        public Task<List<TaskItem>> GetAllByDueDate(DateTime date);
    }
}

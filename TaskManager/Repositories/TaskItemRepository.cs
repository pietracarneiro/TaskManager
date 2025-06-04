using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Repositories.Interfaces;

namespace TaskManager.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly AppDbContext _context;
        public TaskItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> Create(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
            return taskItem;
        }

        public async Task<TaskItem> Delete(int taskItemId)
        {
            var taskItem = await _context.TaskItems.FindAsync(taskItemId);
            if (taskItem == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskItemId} not found.");
            }
            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
            return taskItem;
        }

        public async Task<List<TaskItem>> GetAll()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public Task<List<TaskItem>> GetAllByDueDate(DateTime date)
        {
            var taskItems = _context.TaskItems
                .Where(t => t.DueDate.Date == date.Date)
                .ToListAsync();
            return taskItems;
        }

        public Task<List<TaskItem>> GetAllByStatus(string status)
        {
            var taskItems = _context.TaskItems
                .Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
            return taskItems;
        }

        public async Task<TaskItem> Update(int taskId, TaskItem updatedTaskItem)
        {
            var taskItem = await _context.TaskItems.FindAsync(taskId);
            if (taskItem == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskId} not found.");
            }

            taskItem.Title = updatedTaskItem.Title;
            taskItem.Description = updatedTaskItem.Description;
            taskItem.DueDate = updatedTaskItem.DueDate;
            taskItem.Status = updatedTaskItem.Status;

            await _context.SaveChangesAsync();
            return taskItem;
        }

    }
}

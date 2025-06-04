using TaskManager.Models;
using TaskManager.Repositories.Interfaces;
using TaskManager.Services.Interfaces;

namespace TaskManager.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public TaskItemService(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItem> Create(TaskItem taskItem)
        {
            if (taskItem == null)
            {
                throw new ArgumentNullException("TaskItem cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(taskItem.Title))
            {
                throw new ArgumentException("Title cannot be null or empty.");
            }

            if (taskItem.DueDate < DateTime.Now)
            {
                throw new ArgumentException("Due date cannot be in the past.");
            }

            if (string.IsNullOrWhiteSpace(taskItem.Status))
            {
                taskItem.Status = "Pending";
            }

            taskItem.DueDate = DateTime.Now;
            return await _taskItemRepository.Create(taskItem);
        }

        public async Task<TaskItem> Delete(int taskId)
        {
            if (taskId <= 0)
            {
                throw new ArgumentException("Task ID must be greater than zero.");
            }

            return await _taskItemRepository.Delete(taskId);
        }

        public async Task<List<TaskItem>> GetAll()
        {
            return await _taskItemRepository.GetAll();
        }

        public Task<List<TaskItem>> GetAllByDueDate(DateTime date)
        {
            if (date == null)
            {
                throw new ArgumentNullException("Due date cannot be null.");
            }

            return _taskItemRepository.GetAllByDueDate(date);
        }

        public Task<List<TaskItem>> GetAllByStatus(string status)
        {
            if (status == null)
            {
                throw new ArgumentNullException("Status cannot be null.");
            }

            return _taskItemRepository.GetAllByStatus(status);
        }

        public async Task<TaskItem> Update(int taskId, TaskItem updatedTaskItem)
        {
            if (updatedTaskItem == null)
            {
                throw new ArgumentNullException("UpdatedTaskItem cannot be null.");
            }

            bool noFieldInformed =
                string.IsNullOrWhiteSpace(updatedTaskItem.Title) &&
                string.IsNullOrWhiteSpace(updatedTaskItem.Description) &&
                updatedTaskItem.DueDate == default &&
                string.IsNullOrWhiteSpace(updatedTaskItem.Status);

            if (noFieldInformed)
            {
                throw new ArgumentException("Provide at least one attribute to update the task.");
            }

            return await _taskItemRepository.Update(taskId, updatedTaskItem);
        }

    }
}

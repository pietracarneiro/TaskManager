using AutoMapper;
using TaskManager.DTOs.Requests;
using TaskManager.DTOs.Responses;
using TaskManager.Models;
using TaskManager.Repositories.Interfaces;
using TaskManager.Services.Interfaces;

namespace TaskManager.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;

        public TaskItemService(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
        }

        public async Task<TaskItemResponse> Create(TaskItemRequest taskItemRequest)
        {
            if (taskItemRequest == null)
            {
                throw new ArgumentNullException("TaskItem cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(taskItemRequest.Title))
            {
                throw new ArgumentException("Title cannot be null or empty.");
            }

            if (taskItemRequest.DueDate.HasValue && taskItemRequest.DueDate.Value < DateTime.Now)
            {
                throw new ArgumentException("Due date cannot be in the past.");
            }

            if (string.IsNullOrWhiteSpace(taskItemRequest.Status))
            {
                taskItemRequest.Status = "Pending";
            }

            var taskItem = _mapper.Map<TaskItem>(taskItemRequest);
            taskItem.DueDate = taskItemRequest.DueDate ?? DateTime.Now;
            taskItem.Status = string.IsNullOrWhiteSpace(taskItemRequest.Status) ? "Pending" : taskItemRequest.Status;

            var createdTaskItem = await _taskItemRepository.Create(taskItem);

            return _mapper.Map<TaskItemResponse>(createdTaskItem);
        }

        public async Task<TaskItemResponse> Delete(int taskId)
        {
            if (taskId <= 0)
            {
                throw new ArgumentException("Task ID must be greater than zero.");
            }

            var deletedTaskItem = await _taskItemRepository.Delete(taskId);
            return _mapper.Map<TaskItemResponse>(deletedTaskItem);
        }

        public async Task<List<TaskItemResponse>> GetAll()
        {
            var taskItems = await _taskItemRepository.GetAll();
            return _mapper.Map<List<TaskItemResponse>>(taskItems);
        }

        public async Task<List<TaskItemResponse>> GetAllByDueDate(DateTime date)
        {
            var taskItems = await _taskItemRepository.GetAllByDueDate(date);
            return _mapper.Map<List<TaskItemResponse>>(taskItems);
        }

        public async Task<List<TaskItemResponse>> GetAllByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                throw new ArgumentNullException("Status cannot be null or empty.");
            }

            var taskItems = await _taskItemRepository.GetAllByStatus(status);
            return _mapper.Map<List<TaskItemResponse>>(taskItems);
        }

        public async Task<TaskItemResponse> Update(int taskId, TaskItemRequest updatedTaskItem)
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

            var existingTask = await _taskItemRepository.GetAll();
            var task = existingTask.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
                throw new KeyNotFoundException($"Task with ID {taskId} not found.");

            if (!string.IsNullOrWhiteSpace(updatedTaskItem.Title))
                task.Title = updatedTaskItem.Title;
            if (!string.IsNullOrWhiteSpace(updatedTaskItem.Description))
                task.Description = updatedTaskItem.Description;
            if (updatedTaskItem.DueDate.HasValue)
                task.DueDate = updatedTaskItem.DueDate.Value;
            if (!string.IsNullOrWhiteSpace(updatedTaskItem.Status))
                task.Status = updatedTaskItem.Status;

            var updatedTask = await _taskItemRepository.Update(taskId, task);
            return _mapper.Map<TaskItemResponse>(updatedTask);
        }
    }
}

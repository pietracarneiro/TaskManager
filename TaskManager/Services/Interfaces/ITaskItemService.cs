using TaskManager.DTOs.Requests;
using TaskManager.DTOs.Responses;

namespace TaskManager.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<TaskItemResponse> Create(TaskItemRequest taskItem);
        Task<TaskItemResponse> Update(int taskId, TaskItemRequest updatedTaskItem);
        Task<TaskItemResponse> Delete(int taskId);
        Task<List<TaskItemResponse>> GetAll();
        Task<List<TaskItemResponse>> GetAllByStatus(string status);
        Task<List<TaskItemResponse>> GetAllByDueDate(DateTime date);
    }
}

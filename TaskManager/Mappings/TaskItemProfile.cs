using AutoMapper;
using TaskManager.DTOs.Requests;
using TaskManager.DTOs.Responses;
using TaskManager.Models;

namespace TaskManager.Mappings
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile()
        {
            CreateMap<TaskItem, TaskItemResponse>();
            CreateMap<TaskItemRequest, TaskItem>();
        }
    }
}

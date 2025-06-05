using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs.Requests;
using TaskManager.DTOs.Responses;
using TaskManager.Models;
using TaskManager.Services.Interfaces;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;
        private readonly IMapper _mapper;

        public TaskItemController(ITaskItemService taskItemService, IMapper mapper)
        {
            _taskItemService = taskItemService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItemRequest taskItem)
        {
            if (taskItem == null)
            {
                return new BadRequestObjectResult("TaskItem cannot be null.");
            }
            try
            {
                var createdTask = await _taskItemService.Create(taskItem);
                var taskItemResponse = _mapper.Map<TaskItemResponse>(createdTask);
                return Ok(taskItemResponse);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> Update(int taskId, [FromBody] TaskItemRequest updatedTaskItem)
        {
            if (updatedTaskItem == null)
            {
                return new BadRequestObjectResult("Updated TaskItem cannot be null.");
            }
            try
            {
                var updatedTask = await _taskItemService.Update(taskId, updatedTaskItem);
                var taskItemResponse = _mapper.Map<TaskItemResponse>(updatedTask);
                return Ok(taskItemResponse);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> Delete(int taskId)
        {
            try
            {
                var deletedTask = await _taskItemService.Delete(taskId);
                var taskItemResponse = _mapper.Map<TaskItemResponse>(deletedTask);
                return Ok(taskItemResponse);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _taskItemService.GetAll();
                var taskItemResponses = _mapper.Map<List<TaskItemResponse>>(tasks);
                return Ok(taskItemResponses);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetAllByStatus(string status)
        {
            try
            {
                var tasks = await _taskItemService.GetAllByStatus(status);
                var taskItemResponses = _mapper.Map<List<TaskItemResponse>>(tasks);
                return Ok(taskItemResponses);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Obtém todas as tarefas com a data de vencimento especificada.
        /// </summary>
        /// <param name="date">Data de vencimento no formato yyyy-MM-dd.</param>
        /// <returns>Lista de tarefas com a data de vencimento especificada.</returns>
        [HttpGet("dueDate/{date}")]
        public async Task<IActionResult> GetAllByDueDate(DateTime date)
        {
            try
            {
                var tasks = await _taskItemService.GetAllByDueDate(date);
                var taskItemResponses = _mapper.Map<List<TaskItemResponse>>(tasks);
                return Ok(taskItemResponses);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}

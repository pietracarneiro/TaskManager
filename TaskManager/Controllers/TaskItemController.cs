using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services.Interfaces;

namespace TaskManager.Controllers
{
    public class TaskItemController
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> Create([FromBody] TaskItem taskItem)
        //{
        //    if (taskItem == null)
        //    {
        //        return BadRequestObjectResult("TaskItem cannot be null.");
        //    }
        //    try


        //    {
        //        var createdTask = await _taskItemService.Create(taskItem);
        //        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs.Requests;
using TaskManager.DTOs.Responses;
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

        /// <summary>  
        /// Cria uma nova tarefa.  
        /// </summary>  
        /// <param name="taskItem">  
        /// Dados da tarefa a ser criada. Exemplo de JSON esperado:  
        /// {  
        ///   "title": "Título da tarefa",  
        ///   "description": "Descrição detalhada da tarefa",  
        ///   "dueDate": "2023-10-15T14:30:00", // Data de vencimento no formato ISO 8601  
        ///   "status": "Pendente" // Status da tarefa. Valores possíveis: "Pendente", "Em progresso", "Concluído"  
        /// }  
        /// </param>  
        /// <returns>Retorna a tarefa criada com uma mensagem de sucesso.</returns>  
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
                return Ok(new { Message = "Tarefa criada com sucesso.", Data = taskItemResponse });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>  
        /// Atualiza uma tarefa existente.  
        /// </summary>  
        /// <param name="taskItemId">Id da tarefa a ser atualizada.</param>  
        /// <param name="updatedTaskItem">Dados atualizados da tarefa.</param>  
        /// <returns>Retorna a tarefa atualizada com uma mensagem de sucesso.</returns>  
        [HttpPut("{taskItemId}")]
        public async Task<IActionResult> Update(int taskItemId, [FromBody] TaskItemRequest updatedTaskItem)
        {
            if (updatedTaskItem == null)
            {
                return new BadRequestObjectResult("Updated TaskItem cannot be null.");
            }
            try
            {
                var updatedTask = await _taskItemService.Update(taskItemId, updatedTaskItem);
                var taskItemResponse = _mapper.Map<TaskItemResponse>(updatedTask);
                return Ok(new { Message = "Tarefa atualizada com sucesso.", Data = taskItemResponse });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>  
        /// Exclui uma tarefa existente.  
        /// </summary>  
        /// <param name="taskItemId">Id da tarefa a ser excluída.</param>  
        /// <returns>Retorna a tarefa excluída com uma mensagem de sucesso.</returns>  
        [HttpDelete("{taskItemId}")]
        public async Task<IActionResult> Delete(int taskItemId)
        {
            try
            {
                var deletedTask = await _taskItemService.Delete(taskItemId);
                var taskItemResponse = _mapper.Map<TaskItemResponse>(deletedTask);
                return Ok(new { Message = "Tarefa excluída com sucesso.", Data = taskItemResponse });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>  
        /// Obtém todas as tarefas.  
        /// </summary>  
        /// <returns>Retorna uma lista de todas as tarefas com uma mensagem de sucesso.</returns>  
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _taskItemService.GetAll();
                var taskItemResponse = _mapper.Map<List<TaskItemResponse>>(tasks);
                return Ok(new { Message = "Lista de tarefas obtida com sucesso.", Data = taskItemResponse });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>  
        /// Obtém todas as tarefas com o status especificado.  
        /// </summary>  
        /// <param name="status">Status das tarefas a serem obtidas. Valores possíveis: "Pendente", "Em progresso", "Concluído".</param>  
        /// <returns>Retorna uma lista de tarefas com o status especificado e uma mensagem de sucesso.</returns>  
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetAllByStatus(string status)
        {
            try
            {
                var tasks = await _taskItemService.GetAllByStatus(status);
                var taskItemResponse = _mapper.Map<List<TaskItemResponse>>(tasks);
                return Ok(new { Message = $"Lista de tarefas com status '{status}' obtida com sucesso.", Data = taskItemResponse });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>  
        /// Obtém todas as tarefas com a data de vencimento especificada.  
        /// </summary>  
        /// <param name="date">Informe apenas a data de vencimento no formato: yyyy-MM-dd.</param>  
        /// <returns>Retorna uma lista de tarefas com a data de vencimento especificada e uma mensagem de sucesso.</returns>  
        [HttpGet("dueDate/{date}")]
        public async Task<IActionResult> GetAllByDueDate(DateTime date)
        {
            try
            {
                var tasks = await _taskItemService.GetAllByDueDate(date);
                var taskItemResponse = _mapper.Map<List<TaskItemResponse>>(tasks);
                return Ok(new { Message = $"Lista de tarefas com data de vencimento '{date:yyyy-MM-dd}' obtida com sucesso.", Data = taskItemResponse });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}

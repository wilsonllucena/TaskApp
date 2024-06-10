using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Interfaces;
using TaskApp.Models;

namespace TaskApp.Controllers
{
    [Authorize]
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
     
        public TaskController(ITaskRepository repository)
        {
            _taskRepository = repository;
        }  
        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> GetAll()
        {
            var tasks = await _taskRepository.GetTasks();

            return Ok(tasks);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetUser(string id)
        {
            var task = await _taskRepository.GetTaskById(id);

            return Ok(task);
        }
        
        [HttpPost()]
        public async Task<ActionResult<TaskModel>> AddTask([FromBody] TaskModel task)
        {
            var taskModel = await _taskRepository.AddTask(task);
            return Ok(taskModel);
        }
        
        [HttpPatch("{id}")]
        public async Task<ActionResult<TaskModel>> UpdateTAsk([FromBody] TaskModel task, string id)
        {
            var taskModel = await _taskRepository.UpdateTask(task, id);
            return Ok(taskModel);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> RemoveTask(string id)
        {
            var task = await _taskRepository.RemoveTask(id);

            return Ok(task);
        }
    }
}

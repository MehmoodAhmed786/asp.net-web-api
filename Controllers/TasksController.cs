using Microsoft.AspNetCore.Mvc;
using curd_api.Models;
namespace curd_api.Controllers
{
    [ApiController]
    [Route("tasks")]

    public class TasksController : ControllerBase
    {
        private static List<TaskItem> tasks = new()
        {
            new TaskItem {Id = 1, Title = "Buy milk", Done = false},
            new TaskItem {Id = 2, Title = "Complete assignment", Done= true},
            new TaskItem {Id = 3, Title = "Go for a walk", Done= false}
        };
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(tasks);                                                      
        }
        [HttpGet("{id}")]
        public IActionResult GetTaskByid(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if(task == null)
            {
                return NotFound(new
                {
                    error = $"Tasks with id {id} not found"
                });
            }
            return Ok(task);
        }
        [HttpPost]
        public IActionResult CreateTask([FromBody] CreateTaskRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new
                {
                    error = "Title is required"
                });
            }
            var newTask = new TaskItem
            {
                Id = tasks.Max(t => t.Id) + 1,
                Title = request.Title,
                Done = false
            };
            tasks.Add(newTask);
            return Created($"/tasks/{newTask.Id}", newTask);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] UpdateTaskRequest request)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if(task == null)
            {
                return NotFound(new
                {
                    error = $"Task with id {id} not found"
                });
            }
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new
                {
                error = "Title is required"
                });
            }
            task.Title = request.Title;
            task.Done = request.Done;
            return Ok(task);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTask( int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if(task == null)
            {
                return NotFound(new
                {
                    error = $"Task with {id} not found"
                });
            }
            tasks.Remove(task);
            return NoContent();
        }
    }
}

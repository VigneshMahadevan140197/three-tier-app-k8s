using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = new[]
            {
                new { Id = 1, Title = "Learn Kubernetes", Completed = false },
                new { Id = 2, Title = "Build .NET API", Completed = true }
            };

            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] dynamic task)
        {
            return CreatedAtAction(nameof(GetTasks), new { id = 3 }, task);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TasksController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "DefaultConnection is not configured.");

            var tasks = new List<object>();

            await using var connection =
                new NpgsqlConnection(connectionString);

            await connection.OpenAsync();

            const string sql =
                "SELECT id, title, completed FROM tasks ORDER BY id";

            await using var command =
                new NpgsqlCommand(sql, connection);

            await using var reader =
                await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tasks.Add(new
                {
                    id = reader.GetInt32(0),
                    title = reader.GetString(1),
                    completed = reader.GetBoolean(2)
                });
            }

            return Ok(tasks);
        }
    }
}

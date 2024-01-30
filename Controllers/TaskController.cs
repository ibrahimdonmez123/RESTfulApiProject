
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
    private static List<TaskModel> tasks = new List<TaskModel>
    {
        new TaskModel { Id = 1, Task = "Yazılım calis", Done = false },
        new TaskModel { Id = 2, Task = "Çöpü çıkart", Done = false }
    };

    [HttpGet]
    public ActionResult<IEnumerable<TaskModel>> GetTasks()
    {
        return Ok(new { tasks });
    }

    [HttpGet("{taskId}")]
    public ActionResult<TaskModel> GetTask(int taskId)
    {
        var task = tasks.Find(t => t.Id == taskId);
        if (task == null)
        {
            return NotFound(new { error = "Görev bulunamadı" });
        }
        return Ok(new { task });
    }

    [HttpPost]
    public ActionResult<TaskModel> CreateTask([FromBody] TaskModel newTask)
    {
        if (newTask == null || string.IsNullOrEmpty(newTask.Task))
        {
            return BadRequest(new { error = "Görev eklenmeli" });
        }

        newTask.Id = tasks.Count + 1;
        newTask.Done = false;
        tasks.Add(newTask);

        return CreatedAtAction(nameof(GetTask), new { taskId = newTask.Id }, new { task = newTask });
    }
}

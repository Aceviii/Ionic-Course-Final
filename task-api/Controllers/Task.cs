using TaskModel = task_api.Models.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_api.Models;
using task_api.Migrations;

namespace task_api.Controllers
{
 [Route("api/[controller]")]
 [ApiController]
 public class TasksController : ControllerBase
 {
 private readonly TaskDbContext _context;

 public TasksController(TaskDbContext context)
 {
  _context = context;
 }


 [HttpGet]
 public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
 {
  return await _context.Tasks.ToListAsync();
 }


 [HttpGet("{id}")]
 public async Task<ActionResult<TaskModel>> GetTask(int id)
 {
  var task = await _context.Tasks.FindAsync(id);

  if (task == null)
  {
    return NotFound();
  }

  return task;
 }


 [HttpPut("{id}")]
 public async Task<IActionResult> PutTask(int id, TaskModel task)
 {
  if (id != task.TaskId)
  {
    return BadRequest();
  }

  _context.Entry(task).State = EntityState.Modified;

  try
  {
    await _context.SaveChangesAsync();
  }
  catch (DbUpdateConcurrencyException)
  {
    if (!TaskExists(id))
    {
      return NotFound();
    }
    else
    {
      throw;
    }
  }

  return NoContent();
 }


 [HttpPost]
 public async Task<ActionResult<TaskModel>> PostTask(TaskModel task)
 {
  _context.Tasks.Add(task);
  await _context.SaveChangesAsync();

  return CreatedAtAction("GetTask", new { id = task.TaskId }, task);
 }

 [HttpGet("incomplete-tasks")]
public async Task<ActionResult<IEnumerable<TaskModel>>> GetIncompleteTasks()
{
   return await _context.Tasks.Where(t => !t.Completed).ToListAsync();
}

[HttpGet("completed-tasks")]
public async Task<ActionResult<IEnumerable<TaskModel>>> GetCompletedTasks()
{
   return await _context.Tasks.Where(t => t.Completed).ToListAsync();
}



 [HttpDelete("{id}")]
 public async Task<IActionResult> DeleteTask(int id)
 {
  var task = await _context.Tasks.FindAsync(id);
  if (task == null)
  {
    return NotFound();
  }

  _context.Tasks.Remove(task);
  await _context.SaveChangesAsync();

  return NoContent();
 }

[HttpPatch("{id}/toggle-completion")]
public async Task<IActionResult> ToggleTaskCompletion(int id)
{
    var task = await _context.Tasks.FindAsync(id);

    if (task == null)
    {
        return NotFound();
    }

    task.Completed = !task.Completed;

    try
    {
        await _context.SaveChangesAsync();
        return Ok(task);
    }
    catch (DbUpdateConcurrencyException)
    {
        return StatusCode(500, "Failed to update task completion status.");
    }
}


 private bool TaskExists(int id)
 {
  return _context.Tasks.Any(e => e.TaskId == id);
 }
 }
}

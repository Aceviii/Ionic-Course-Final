using task_api.Migrations;
using task_api.Models;

namespace task_api.Repositories
{
    public class TaskRepository : ITaskRepository 
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public IEnumerable<task_api.Models.Task> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public task_api.Models.Task GetTaskById(int taskId)
        {
            return _context.Tasks.SingleOrDefault(c => c.TaskId == taskId);
        }

        public task_api.Models.Task CreateTask(task_api.Models.Task newTask)
        {
            _context.Tasks.Add(newTask);
            _context.SaveChanges();
            return newTask;
        }

        public task_api.Models.Task UpdateTask(task_api.Models.Task updatedTask)
        {
            var originalTask = _context.Tasks.Find(updatedTask.TaskId);
            if (originalTask != null)
            {
                originalTask.Title = updatedTask.Title;
                originalTask.Completed = updatedTask.Completed;
                _context.SaveChanges();
            }
            return originalTask;
        }

        public void DeleteTaskById(int taskId)
        {
            var task = _context.Tasks.Find(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task); 
                _context.SaveChanges(); 
            }
        }
    }
}

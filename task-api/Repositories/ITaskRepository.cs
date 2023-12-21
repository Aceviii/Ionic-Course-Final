using task_api.Models;

namespace task_api.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<task_api.Models.Task> GetAllTasks();
        task_api.Models.Task GetTaskById(int taskId);
        task_api.Models.Task CreateTask(task_api.Models.Task newTask);
        task_api.Models.Task UpdateTask(task_api.Models.Task updatedTask);
        void DeleteTaskById(int taskId);
    }
}


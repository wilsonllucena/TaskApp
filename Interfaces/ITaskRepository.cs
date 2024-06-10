using TaskApp.Models;

namespace TaskApp.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskModel>> GetTasks();
    Task<TaskModel> GetTaskById(string id);
    Task<TaskModel> AddTask(TaskModel task);
    Task<TaskModel> UpdateTask(TaskModel task, string id);
    Task<bool> RemoveTask(string id);
}
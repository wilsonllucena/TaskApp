using Microsoft.EntityFrameworkCore;
using TaskApp.Data;
using TaskApp.Interfaces;
using TaskApp.Models;

namespace TaskApp.Repositories;

public class TaskRepository: ITaskRepository
{
    private readonly AppDbContext _dbContext;
    // Construtor da class 
    public TaskRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<List<TaskModel>> GetTasks()
    {
        return await _dbContext.Tasks.Include(model => model.User).ToListAsync();
    }

    public async Task<TaskModel> GetTaskById(string id)
    {
        return await _dbContext.Tasks
            .Include(model => model.User)
            .FirstOrDefaultAsync(model => model.Id.ToString() == id);
    }

    public async Task<TaskModel> AddTask(TaskModel task)
    {
         await _dbContext.Tasks.AddAsync(task);
         await _dbContext.SaveChangesAsync();

         return task;
    }

    public async Task<TaskModel> UpdateTask(TaskModel task, string id)
    {
        var taskExist = await GetTaskById(id);

        if (taskExist ==  null)
        {
            throw new Exception($"task to id:{id} not found");
        }

        taskExist.Name = task.Name ?? taskExist.Name;
        taskExist.Description = task.Description ?? taskExist.Description;
        taskExist.Status = task.Status;
        taskExist.UserId = task.UserId;
        taskExist.User = task.User;

        _dbContext.Tasks.Update(taskExist);
        await _dbContext.SaveChangesAsync();
        return taskExist;
    }

    public async Task<bool> RemoveTask(string id)
    {
        var taskExist = await GetTaskById(id);

        if (taskExist ==  null)
        {
            throw new Exception($"Task to id:{id} not found");
        }

        _dbContext.Tasks.Remove(taskExist);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
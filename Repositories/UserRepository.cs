using Microsoft.EntityFrameworkCore;
using TaskApp.Data;
using TaskApp.Interfaces;
using TaskApp.Models;

namespace TaskApp.Repositories;

public class UserRepository: IUserRepository
{
    private readonly AppDbContext _dbContext;
    // Construtor da class 
    public UserRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<List<UserModel>> getUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<UserModel> getUserById(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<UserModel> addUser(UserModel user)
    {
         await _dbContext.Users.AddAsync(user);
         await _dbContext.SaveChangesAsync();

         return user;
    }

    public async Task<UserModel> updateUser(UserModel user, int id)
    {
        var userExist = await getUserById(id);

        if (userExist ==  null)
        {
            throw new Exception($"User to id:{id} not found");
        }

        userExist.Name = user.Name ?? userExist.Name;
        userExist.Email = user.Email ?? userExist.Email;
        userExist.Active = user.Active;

        _dbContext.Users.Update(userExist);
        await _dbContext.SaveChangesAsync();
        return userExist;
    }

    public async Task<bool> removeUser(int id)
    {
        var userExist = await getUserById(id);

        if (userExist ==  null)
        {
            throw new Exception($"User to id:{id} not found");
        }

        _dbContext.Users.Remove(userExist);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
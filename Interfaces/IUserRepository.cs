using TaskApp.Models;

namespace TaskApp.Interfaces;

public interface IUserRepository
{
    Task<List<UserModel>> getUsers();
    Task<UserModel> getUserById(int id);
    Task<UserModel> addUser(UserModel user);
    Task<UserModel> updateUser(UserModel user, int id);
    Task<bool> removeUser(int id);
}
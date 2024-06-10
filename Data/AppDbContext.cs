using System.Configuration;
using Microsoft.EntityFrameworkCore;
using TaskApp.Data.Mapper;
using TaskApp.Models;

namespace TaskApp.Data;

public class AppDbContext: DbContext
{
    // Auqi vai configuração do banco de dados 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    // Configurando as tabelas para serem criadas 
    public DbSet<UserModel> Users { get; set; } 
    public DbSet<TaskModel> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMapper());
        modelBuilder.ApplyConfiguration(new TaskMapper());
        
        base.OnModelCreating(modelBuilder);
    }
}
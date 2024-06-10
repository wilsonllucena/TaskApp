using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskApp.Models;

namespace TaskApp.Data.Mapper;

public class TaskMapper: IEntityTypeConfiguration<TaskModel>
{
    public void Configure(EntityTypeBuilder<TaskModel> builder)
    {
        builder.HasKey(model => model.Id);
        builder.Property(model => model.Name).IsRequired().HasMaxLength(255);
        builder.Property(model => model.Description).HasMaxLength(1000);
        builder.Property(model => model.Status).IsRequired();
        builder.Property(model => model.UserId);
        builder.HasOne(model => model.User);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskApp.Models;

namespace TaskApp.Data.Mapper;

public class UserMapper: IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(model => model.Id);
        builder.Property(model => model.Name).IsRequired().HasMaxLength(255);
        builder.Property(model => model.Email).IsRequired().HasMaxLength(255);
        builder.Property(model => model.Active);
    }
}
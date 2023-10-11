using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities.General;

namespace TodoApp.Infrastructure.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var guid1 = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfb");
            var guid2 = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfc");

            var usersInit = new List<User>
            {
                new User
                {
                    Id = guid1,
                    Name = "user01",
                    Password = "user01",
                    CreateDate = DateTime.UtcNow
                },
                new User
                {
                    Id = guid2,
                    Name = "user02",
                    Password = "user02",
                    CreateDate = DateTime.UtcNow
                }
            };

            builder.ToTable(nameof(User));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(p => p.CreateDate);
            builder.Property(p => p.UpdateDate);
            builder.HasMany(s => s.ToDos)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(usersInit);
        }
    }
}

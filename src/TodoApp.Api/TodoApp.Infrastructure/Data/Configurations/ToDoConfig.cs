using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities.General;

namespace TodoApp.Infrastructure.Data.Configurations
{
    public class ToDoConfig : IEntityTypeConfiguration<ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> builder)
        {
            var guid1 = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfb");
            var guid2 = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfc");

            var toDosInit = new List<ToDo>
            {
                new ToDo
                {
                    Id = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfa"),
                    UserId = guid1,
                    Title = "Milk",
                    CreationDate = DateTime.UtcNow
                },
                new ToDo
                {
                    Id = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"),
                    UserId = guid1,
                    Title = "Dog food",
                    CreationDate = DateTime.UtcNow
                },
                new ToDo
                {
                    Id = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"),
                    UserId = guid2,
                    Title = "Kubernetes",
                    CreationDate = DateTime.UtcNow
                },
                new ToDo
                {
                    Id = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfd"),
                    UserId = guid2,
                    Title = "New Relic",
                    CreationDate = DateTime.UtcNow
                },
                new ToDo
                {
                    Id = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfe"),
                    UserId = guid2,
                    Title = "Azure Databases",
                    CreationDate = DateTime.UtcNow
                }
            };

            builder.ToTable(nameof(ToDo));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(p => p.CreationDate);
            builder.Property(p => p.UpdateDate);
            builder.HasOne(s => s.User).WithMany(s => s.ToDos).HasForeignKey(s => s.UserId);
            builder.HasData(toDosInit);
        }
    }
}

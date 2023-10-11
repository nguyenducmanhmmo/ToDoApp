using TodoApp.Core.Entities.General;
using TodoApp.Core.Interfaces.IRepositories;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repositories
{
    public class TodoRepository : BaseRepository<ToDo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }
    }
}

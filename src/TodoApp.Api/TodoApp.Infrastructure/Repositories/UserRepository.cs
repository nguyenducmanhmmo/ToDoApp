using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities.Business;
using TodoApp.Core.Entities.General;
using TodoApp.Core.Interfaces.IRepositories;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoDbContext _dbContext;
        public UserRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Authenticate(AuthenticateRequest model)
        {
            var user =  await _dbContext.Users.SingleOrDefaultAsync(x => x.Name == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            return user;
        }


    }
}

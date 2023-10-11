using TodoApp.Core.Entities.Business;
using TodoApp.Core.Entities.General;

namespace TodoApp.Core.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User> Authenticate(AuthenticateRequest model);
    }
}

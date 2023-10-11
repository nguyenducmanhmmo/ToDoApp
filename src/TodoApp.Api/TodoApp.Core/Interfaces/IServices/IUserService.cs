using TodoApp.Core.Entities.Business;

namespace UserApp.Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }
}

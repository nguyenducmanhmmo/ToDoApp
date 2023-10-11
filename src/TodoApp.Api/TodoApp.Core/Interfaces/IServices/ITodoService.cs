using TodoApp.Core.Entities.Business;

namespace TodoApp.Core.Interfaces.IServices
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoViewModel>> GetTodos();
        Task<PaginatedDataViewModel<TodoViewModel>> GetPaginatedTodos(int pageNumber, int pageSize);
        Task<TodoViewModel> GetTodo(Guid id);
        Task<bool> IsExists(string key, string value);
        Task<bool> IsExistsForUpdate(Guid id, string key, string value);
        Task<TodoViewModel> Create(TodoViewModel model);
        Task<bool> Update(TodoViewModel model);
        Task<bool> Delete(Guid id);
    }
}

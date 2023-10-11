using Microsoft.AspNetCore.Http;
using TodoApp.Core.Constant;
using TodoApp.Core.Entities.Business;
using TodoApp.Core.Entities.General;
using TodoApp.Core.Interfaces.IMapper;
using TodoApp.Core.Interfaces.IRepositories;
using TodoApp.Core.Interfaces.IServices;

namespace TodoApp.Core.Services
{
    public class TodoService : ITodoService
    {
        private readonly IBaseMapper<ToDo, TodoViewModel> _TodoViewModelMapper;
        private readonly IBaseMapper<TodoViewModel, ToDo> _TodoMapper;
        private readonly ITodoRepository _TodoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TodoService(
            IBaseMapper<ToDo, TodoViewModel> TodoViewModelMapper,
            IBaseMapper<TodoViewModel, ToDo> TodoMapper,
            ITodoRepository TodoRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _TodoMapper = TodoMapper;
            _TodoViewModelMapper = TodoViewModelMapper;
            _TodoRepository = TodoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<TodoViewModel>> GetTodos()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(AppConstant.IdClaim)?.Value;
            return _TodoViewModelMapper.MapList(await _TodoRepository.GetAll(x => x.UserId.ToString() == userId));
        }

        public async Task<PaginatedDataViewModel<TodoViewModel>> GetPaginatedTodos(int pageNumber, int pageSize)
        {
            //Get peginated data
            var paginatedData = await _TodoRepository.GetPaginatedData(pageNumber, pageSize);

            //Map data with ViewModel
            var mappedData = _TodoViewModelMapper.MapList(paginatedData.Data);

            var paginatedDataViewModel = new PaginatedDataViewModel<TodoViewModel>(mappedData.ToList(), paginatedData.TotalCount);

            return paginatedDataViewModel;
        }

        public async Task<TodoViewModel> GetTodo(Guid id)
        {
            return _TodoViewModelMapper.MapModel(await _TodoRepository.GetById(id));
        }

        public async Task<bool> IsExists(string key, string value)
        {
            return await _TodoRepository.IsExists(key, value);
        }

        public async Task<bool> IsExistsForUpdate(Guid id, string key, string value)
        {
            return await _TodoRepository.IsExistsForUpdate(id, key, value);
        }

        public async Task<TodoViewModel> Create(TodoViewModel model)
        {
            model.UserId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(AppConstant.IdClaim)?.Value);
            //Mapping through AutoMapper
            var entity = _TodoMapper.MapModel(model);
            entity.CreateDate = DateTime.UtcNow;

            return _TodoViewModelMapper.MapModel(await _TodoRepository.Create(entity));
        }

        public async Task<bool> Update(TodoViewModel model)
        {
            var existingData = await _TodoRepository.GetById(model.Id);
            if (existingData is null) return false;

            //Manual mapping
            existingData.Title = model.Title;
            existingData.UpdateDate = DateTime.UtcNow;

            await _TodoRepository.Update(existingData);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _TodoRepository.GetById(id);
            if (entity is null) return false;
            await _TodoRepository.Delete(entity);
            return true;
        }
    }
}

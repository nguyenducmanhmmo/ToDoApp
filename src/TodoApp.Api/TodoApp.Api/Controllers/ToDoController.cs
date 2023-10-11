using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApp.Core.Constant;
using TodoApp.Core.Entities.Business;
using TodoApp.Core.Interfaces.IServices;
namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly ITodoService _toDoService;

        public ToDoController(ILogger<ToDoController> logger, ITodoService toDoService)
        {
            _logger = logger;
            _toDoService = toDoService;
        }

        /// <summary>
        /// Return list of Todos of the current use
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Return list of Todos of the current use")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoViewModel>>> GetAllToDosByUserIdAsync()
        {         
            try
            {                
                var toDos = await _toDoService.GetTodos();
                return toDos is null ? NotFound() : Ok(toDos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving toDos");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get ToDo Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get ToDo Detail")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TodoViewModel>> GetTodoDetailByIdAsync(Guid id)
        {
            try
            {
                var toDo = await _toDoService.GetTodo(id);

                return toDo is null ? NotFound() : Ok(toDo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the todo");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Create new Todo for the current user
        /// </summary>
        /// <param name="toDo"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Create new Todo for the current user")]
        [HttpPost]
        public async Task<ActionResult<TodoViewModel>> CreateNewTodoAsync([FromBody] TodoViewModel toDo)
        {
            try
            {
                var newToDo= await _toDoService.Create(toDo);
                return Ok(newToDo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding the toDo");
                ModelState.AddModelError("Error", $"An error occurred while adding the product- " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update Todo Detail
        /// </summary>
        /// <param name="id"></param>
        /// <param name="toDo"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Update Todo Detail")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateTodoAsync(Guid id, [FromBody] TodoViewModel toDo)
        {
            try
            {
                toDo.Id = id;
                var isUpdated = await _toDoService.Update(toDo);

                return isUpdated ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the toDo");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete Todo Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Delete Todo Item")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTodoAsync(Guid id)
        {
            try
            {
                var isDeleted = await _toDoService.Delete(id);
                return isDeleted ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the toDo");
                return StatusCode(500, ex.Message);
            }
        }
    }
}

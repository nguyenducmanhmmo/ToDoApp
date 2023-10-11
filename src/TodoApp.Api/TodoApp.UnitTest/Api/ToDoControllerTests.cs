using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Core.Entities.Business;
using TodoApp.Core.Interfaces.IServices;


namespace TodoApp.UnitTest.Api
{
    public class ToDoControllerTests
    {
        private Mock<ITodoService> _toDoServiceMock;
        private Mock<ILogger<ToDoController>> _loggerMock;
        private ToDoController _toDoController;

        [SetUp]
        public void Setup()
        {
            _toDoServiceMock = new Mock<ITodoService>();
            _loggerMock = new Mock<ILogger<ToDoController>>();
            _toDoController = new ToDoController(_loggerMock.Object, _toDoServiceMock.Object);
        }

        [Test]
        public async Task GetAllToDosByUserIdAsync_Valid_ReturnsActionResultWithListOfTodos()
        {
            // Arrange
            var userId = "7c2196e4-9d06-4574-a212-d4bdef0a4bf1";
            var toDos = new List<TodoViewModel>
            {
                new TodoViewModel { Id = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa"), Title = "P001", CreationDate = DateTime.Now, UserId = Guid.Parse(userId)},
                new TodoViewModel { Id = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"), Title = "P002", CreationDate = DateTime.Now, UserId = Guid.Parse(userId)}
            };

            _toDoServiceMock.Setup(service => service.GetTodos())
                               .ReturnsAsync(toDos);
            // Act
            ActionResult<IEnumerable<TodoViewModel>> actionResult = await _toDoController.GetAllToDosByUserIdAsync();
            var result = (actionResult.Result as ObjectResult).Value;
            // Assert
            Assert.That((result as IEnumerable<TodoViewModel>).Count, Is.EqualTo(toDos.Count));
        }

        [Test]
        public async Task GetTodoDetailByIdAsync_Valid__ReturnsActionResultWithTodo()
        {
            // Arrange
            var userId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bf1");
            var todoId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa");
            var toDo = new TodoViewModel { Id = todoId, Title = "P001", CreationDate = DateTime.Now, UserId = userId };

            _toDoServiceMock.Setup(service => service.GetTodo(todoId)).ReturnsAsync(toDo);
            // Act
            ActionResult<TodoViewModel> actionResult = await _toDoController.GetTodoDetailByIdAsync(todoId);
            var result = (actionResult.Result as ObjectResult).Value;
            // Assert
            Assert.That(result, Is.EqualTo(toDo));
        }

        [Test]
        public async Task CreateNewTodoAsync_ValidToDo_ReturnsActionResultWithTodo()
        {
            // Arrange
            var userId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bf1");
            var todoId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa");
            var toDo = new TodoViewModel { Id = todoId, Title = "P001", CreationDate = DateTime.Now, UserId = userId };

            _toDoServiceMock.Setup(service => service.Create(toDo)).ReturnsAsync(toDo);
            // Act
            ActionResult<TodoViewModel> actionResult = await _toDoController.CreateNewTodoAsync(toDo);
            var result = (actionResult.Result as ObjectResult).Value;
            // Assert
            Assert.That(result, Is.EqualTo(toDo));
        }

        [Test]
        public async Task UpdateTodoAsync_ValidTodo_ReturnsOkActionResult()
        {
            // Arrange
            var userId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bf1");
            var todoId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa");
            var toDo = new TodoViewModel { Id = todoId, Title = "P001", CreationDate = DateTime.Now, UserId = userId };

            _toDoServiceMock.Setup(service => service.Update(toDo)).ReturnsAsync(true);
            // Act
            var actionResult = await _toDoController.UpdateTodoAsync(todoId, toDo);
            // Assert
            Assert.IsInstanceOf<OkResult>(actionResult);
        }

        [Test]
        public async Task DeleteTodoAsync_ValidTodo_ReturnsOkActionResult()
        {
            // Arrange
            var todoId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa");
            _toDoServiceMock.Setup(service => service.Delete(todoId)).ReturnsAsync(true);
            // Act
            var actionResult = await _toDoController.DeleteTodoAsync(todoId);
            // Assert
            Assert.IsInstanceOf<OkResult>(actionResult);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using TodoApp.Core.Constant;
using TodoApp.Core.Entities.Business;
using TodoApp.Core.Entities.General;
using TodoApp.Core.Interfaces.IMapper;
using TodoApp.Core.Interfaces.IRepositories;
using TodoApp.Core.Services;

namespace TodoApp.UnitTest.Core
{
    public class TodoServiceTests
    {
        private Mock<IBaseMapper<ToDo, TodoViewModel>> _toDoViewModelMapperMock;
        private Mock<IBaseMapper<TodoViewModel, ToDo>> _toDoMapperMock;
        private Mock<ITodoRepository> _toDoRepositoryMock;
        private Mock<IHttpContextAccessor> _httpContextMock;

        [SetUp]
        public void Setup()
        {
            _toDoViewModelMapperMock = new Mock<IBaseMapper<ToDo, TodoViewModel>>();
            _toDoMapperMock = new Mock<IBaseMapper<TodoViewModel, ToDo>>();
            _toDoRepositoryMock = new Mock<ITodoRepository>();
            _httpContextMock = new Mock<IHttpContextAccessor>();
        }

        [Test]
        public async Task GetTodosAsync_Valid__ReturnsListOfTodoViewModels()
        {
            // Arrange
            var todoService = new TodoService(
                _toDoViewModelMapperMock.Object,
                _toDoMapperMock.Object,
                _toDoRepositoryMock.Object,
                _httpContextMock.Object);

            var todoViewModel = new TodoViewModel
            {
                Title = "P001"
            };

            var todo = new ToDo
            {
                Title = "P001"
            };

            var listTodoViewModel = new List<TodoViewModel> { todoViewModel };

            var listTodo = new List<ToDo> { todo };

            var userId = "7c2196e4-9d06-4574-a212-d4bdef0a4bfa";

            var claims = new List<Claim>
            {
            new Claim(AppConstant.IdClaim, userId)
            // You can add more claims if needed
            };

            var claimsIdentity = new ClaimsIdentity(claims, "test");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


            _toDoMapperMock.Setup(mapper => mapper.MapList(listTodoViewModel))
                              .Returns(listTodo);

            _toDoRepositoryMock.Setup(repo => repo.GetAll(x => x.UserId.ToString() == userId))
                                  .ReturnsAsync(listTodo);

            _toDoViewModelMapperMock.Setup(mapper => mapper.MapList(listTodo))
                                       .Returns(listTodoViewModel);

            _httpContextMock.Setup(h => h.HttpContext.User).Returns(claimsPrincipal);


            // Act
            var result = await todoService.GetTodos();

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(listTodoViewModel.Count));
        }


        [Test]
        public async Task CreateTodoAsync_ValidTodo_ReturnsCreatedTodoViewModel()
        {
            // Arrange
            var todoService = new TodoService(
                _toDoViewModelMapperMock.Object,
                _toDoMapperMock.Object,
                _toDoRepositoryMock.Object,
                _httpContextMock.Object);

            var newTodoViewModel = new TodoViewModel
            {
                Title = "P001"
            };

            var createdTodo = new ToDo
            {
                Title = "P001"
            };

            var userId = "7c2196e4-9d06-4574-a212-d4bdef0a4bfa";

            var claims = new List<Claim>
            {
            new Claim(AppConstant.IdClaim, userId)
            // You can add more claims if needed
            };

            var claimsIdentity = new ClaimsIdentity(claims, "test");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


            _toDoMapperMock.Setup(mapper => mapper.MapModel(newTodoViewModel))
                              .Returns(createdTodo);

            _toDoRepositoryMock.Setup(repo => repo.Create(createdTodo))
                                  .ReturnsAsync(createdTodo);

            _toDoViewModelMapperMock.Setup(mapper => mapper.MapModel(createdTodo))
                                       .Returns(newTodoViewModel);

            _httpContextMock.Setup(h => h.HttpContext.User).Returns(claimsPrincipal);


            // Act
            var result = await todoService.Create(newTodoViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Title, Is.EqualTo(newTodoViewModel.Title));
        }

        [Test]
        public async Task UpdateTodoAsync_ValidTodo_ReturnsTrue()
        {
            // Arrange
            var todoService = new TodoService(
                _toDoViewModelMapperMock.Object,
                _toDoMapperMock.Object,
                _toDoRepositoryMock.Object,
                _httpContextMock.Object);

            var updateTodoViewModel = new TodoViewModel
            {
                Title = "P001",
                Id = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa"),
                UserId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa")
            };

            _toDoRepositoryMock.Setup(repo => repo.GetById(updateTodoViewModel.Id)).ReturnsAsync(new ToDo {
                Title = "P001",
                Id = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa"),
                UserId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa")
            });
            // Act
            var result = await todoService.Update(updateTodoViewModel);
            

            // Assert
            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteTodoAsync_ValidTodo_ReturnsTrue()
        {
            // Arrange
            var todoService = new TodoService(
                _toDoViewModelMapperMock.Object,
                _toDoMapperMock.Object,
                _toDoRepositoryMock.Object,
                _httpContextMock.Object);

            var deleteTodoViewModel = new ToDo
            {
                Title = "P001",
                Id = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa"),
                UserId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfa")
            };

            _toDoRepositoryMock.Setup(repo => repo.GetById(deleteTodoViewModel.Id)).ReturnsAsync(deleteTodoViewModel);
            // Act
            var result = await todoService.Delete(deleteTodoViewModel.Id);
            // Assert
            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo(true));
        }
    }
}

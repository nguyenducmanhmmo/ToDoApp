using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Core.Entities.General;

namespace TodoApp.UnitTest.Infrastructure
{
    public class TodoRepositoryTests
    {
        private Mock<TodoDbContext> _dbContextMock;
        private TodoRepository _toDoRepository;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<TodoDbContext>(new DbContextOptions<TodoDbContext>());
            _toDoRepository = new TodoRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_Valid_ReturnTodo()
        {
            var toDoId = new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfa");
            var todo = new ToDo
            {
                Id = toDoId,
                Title = "P001"
            };

            var todoDbSetMock = new Mock<DbSet<ToDo>>();

            _dbContextMock.Setup(db => db.Set<ToDo>())
                          .Returns(todoDbSetMock.Object);

            todoDbSetMock.Setup(dbSet => dbSet.FindAsync(toDoId)).ReturnsAsync(todo);

            // Act
            var result = await _toDoRepository.GetById(toDoId);


            // Assert
            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo(todo));
        }

        [Test]
        public async Task AddAsync_ValidTodo_ReturnsAddedTodo()
        {

            // Arrange
            var newTodo= new ToDo
            {
                Title = "P001"
            };

            var todoDbSetMock = new Mock<DbSet<ToDo>>();

            _dbContextMock.Setup(db => db.Set<ToDo>())
                          .Returns(todoDbSetMock.Object);

            todoDbSetMock.Setup(dbSet => dbSet.AddAsync(newTodo, default))
                            .ReturnsAsync((EntityEntry<ToDo>)null);

            // Act
            var result = await _toDoRepository.Create(newTodo);


            // Assert
            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo(newTodo));
        }

        [Test]
        public async Task UpdateAsync_Valid_SaveChangesAsyncCallOnce()
        {

            // Arrange
            var updateTodo = new ToDo
            {
                Title = "P001"
            };

            var todoDbSetMock = new Mock<DbSet<ToDo>>();

            _dbContextMock.Setup(db => db.Set<ToDo>())
                          .Returns(todoDbSetMock.Object);

            todoDbSetMock.Setup(dbSet => dbSet.Update(updateTodo));

            // Act
            await _toDoRepository.Update(updateTodo);


            // Assert
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_Valid_SaveChangesAsyncCallOnce()
        {

            // Arrange
            var deleteTodo = new ToDo
            {
                Title = "P001"
            };

            var todoDbSetMock = new Mock<DbSet<ToDo>>();

            _dbContextMock.Setup(db => db.Set<ToDo>())
                          .Returns(todoDbSetMock.Object);

            todoDbSetMock.Setup(dbSet => dbSet.Remove(deleteTodo));

            // Act
            await _toDoRepository.Delete(deleteTodo);


            // Assert
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }
    }
}

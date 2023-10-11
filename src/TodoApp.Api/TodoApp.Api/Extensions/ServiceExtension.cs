using AutoMapper;
using TodoApp.Core.Entities.Business;
using TodoApp.Core.Entities.General;
using TodoApp.Core.Interfaces.IMapper;
using TodoApp.Core.Interfaces.IRepositories;
using TodoApp.Core.Interfaces.IServices;
using TodoApp.Core.Interfaces.IUnitOfWork;
using TodoApp.Core.Mapper;
using TodoApp.Core.Services;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Infrastructure.UnitOfWork;
using UserApp.Core.Interfaces.IServices;
using UserApp.Core.Services;

namespace TodoApp.Api.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<IUserService, UserService>();

            #endregion

            #region Repositories
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            #endregion

            #region UnitofWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region HttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region Mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ToDo, TodoViewModel>();
                cfg.CreateMap<TodoViewModel, ToDo>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            IMapper mapper = configuration.CreateMapper();

            // Register the IMapperService implementation with your dependency injection container
            services.AddSingleton<IBaseMapper<ToDo, TodoViewModel>>(new BaseMapper<ToDo, TodoViewModel>(mapper));
            services.AddSingleton<IBaseMapper<TodoViewModel, ToDo>>(new BaseMapper<TodoViewModel, ToDo>(mapper));
            services.AddSingleton<IBaseMapper<User, UserViewModel>>(new BaseMapper<User, UserViewModel>(mapper));
            services.AddSingleton<IBaseMapper<UserViewModel, User>>(new BaseMapper<UserViewModel, User>(mapper));

            #endregion

            #region Cors
            services.AddCors();
            #endregion

            #region HealthChecks
            services.AddHealthChecks();
            #endregion

            return services;
        }
    }
}

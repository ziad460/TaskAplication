using Greenz.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Task.Core.Interfaces;
using Task.Infrastructure.ExamRepository;
using Task.Infrastructure.StudentRepository;
using Task.Infrastructure.UserRepository;
using Task.Services.ExamService;
using Task.Services.StudentService;
using Task.Services.UserService;

namespace Task.WebApplication
{
    public static class ServiceCollectionExtensions
    {
        public static void ServiceLibrary(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IExamService, ExamService>();
            services.AddTransient<IExamRepository, ExamRepository>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IStudentRepository, StudentRepository>();
        }

        public static void AddUnitOfWork<TContext>(this IServiceCollection services, IConfiguration Configuration)
            where TContext : DbContext
        {
            services.AddScoped<IUnityOfWork<TContext>, UnityOfWork<TContext>>();
            services.AddScoped<IUnityOfWork, UnityOfWork<TContext>>();
        }

        public static void AddUnitOfWork<TContext1, TContext2>(this IServiceCollection services, IConfiguration Configuration)
            where TContext1 : DbContext
            where TContext2 : DbContext
        {
            services.AddScoped<IUnityOfWork<TContext1>, UnityOfWork<TContext1>>();
            services.AddScoped<IUnityOfWork<TContext2>, UnityOfWork<TContext2>>();
        }

        public static void AddUnitOfWork<TContext1, TContext2, TContext3>(this IServiceCollection services, IConfiguration Configuration)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
        {
            services.AddScoped<IUnityOfWork<TContext1>, UnityOfWork<TContext1>>();
            services.AddScoped<IUnityOfWork<TContext2>, UnityOfWork<TContext2>>();
            services.AddScoped<IUnityOfWork<TContext3>, UnityOfWork<TContext3>>();

        }
    }
}

using TODOApi.Interfaces;
using TODOApi.Services;

namespace TODOApi
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) 
        {
            return services
                .AddTransient<IItemRepository, ItemRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IUserPasswordRepository, UserPasswordRepository>();
        }
    }
}

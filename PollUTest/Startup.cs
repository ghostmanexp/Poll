using Microsoft.Extensions.DependencyInjection;
using Poll;
using Poll.Interfaces;
using Poll.Services;

namespace PollUTest
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Main>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}

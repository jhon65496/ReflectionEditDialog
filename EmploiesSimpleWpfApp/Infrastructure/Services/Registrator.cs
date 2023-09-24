using Microsoft.Extensions.DependencyInjection;
using EmploiesSimpleWpfApp.Infrastructure.Services.Interfaces;

namespace EmploiesSimpleWpfApp.Infrastructure.Services
{
    internal static class Registrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IUserDialog, AppWindowUserDialogService>();
    }
}

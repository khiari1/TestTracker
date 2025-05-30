using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tsi.Extensions.Identity.Core
{
    public static class TsiIdentityCoreExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services, Type userType,Type permissiontype)
        {
            services.TryAddScoped(typeof(TsiUserManager<,>).MakeGenericType(userType,permissiontype));
            services.TryAddScoped(typeof(TsiGroupManager<,>));
            return services;
        }
    }
}

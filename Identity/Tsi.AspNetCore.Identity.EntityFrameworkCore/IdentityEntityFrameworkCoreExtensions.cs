using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Security;
using Tsi.AspNetCore.Identity.Abstractions;
using Tsi.Extensions.Identity.Stores;

namespace Tsi.AspNetCore.Identity.EntityFrameworkCore
{
    public static class IdentityEntityFrameworkCoreExtensions
    {

        public static void AddTsiIdentityStore(this IServiceCollection services, Type userType,Type contextType,Type permissionType)
        {
            services.AddScoped(typeof(IUserStore<,>).MakeGenericType(userType,permissionType), 
                typeof(UserStore<,,>).MakeGenericType(userType, contextType, permissionType));


            var groupType = typeof(TsiIdentityGroup);
            var usergroopType = typeof(TsiIdentityUserGroup);
            var permissiontype = typeof(TsiIdentityPermission);

            services.AddScoped(typeof(IGroupStore<,>).MakeGenericType(groupType,permissiontype),
                typeof(GroupStore<,,,,>).MakeGenericType(groupType, contextType,typeof(string),usergroopType,permissiontype));


        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.AspNetCore.Identity.AzureAD
{
    public static class UserAzureADManagerExtensions
    {
        public static IServiceCollection AddUserAzureAD(this IServiceCollection service)
        {
            service.TryAddScoped(typeof(UserAzureADManager<,>));
            return service;
        }
    }
}

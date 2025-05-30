using Microsoft.AspNetCore.Authorization;



namespace Tsi.Erp.TestTracker.Api.Security
{
    [AttributeUsage(AttributeTargets.Method)]
    

    public class TsiAuthorizeAttribute : AuthorizeAttribute
    {
        public TsiAuthorizeAttribute(Permissions requiredPermission)
        {
            Policy = requiredPermission.ToString();
        }
        public TsiAuthorizeAttribute()
       {   }
    }
}
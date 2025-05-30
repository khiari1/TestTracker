using Microsoft.AspNetCore.Authorization;

namespace Tsi.Erp.TestTracker.Api.Security
{
    public class PermissionRequirement : IAuthorizationRequirement
    {

        public List<Permissions> RequiredPermissions { get; set; }

        public PermissionRequirement(Permissions requiredPermission)
        {
            RequiredPermission = requiredPermission;
            RequiredPermissions = new List<Permissions> { requiredPermission };
        }

        public PermissionRequirement()
        {
        }

        public Permissions RequiredPermission { get; }
        
    }
}

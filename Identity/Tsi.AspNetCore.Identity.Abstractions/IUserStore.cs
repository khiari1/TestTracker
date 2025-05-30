



using System.Linq.Extensions;

public interface IUserStore<TUser,TPermission>
    where TUser: class
   where TPermission : class
{
    Task<IList<TUser>> GetAsync();
    Task<IList<TUser>> GetAsync(Query filter);
    Task CreateAsync(TUser user);
    Task UpdateAsync(TUser user);
    Task DeleteAsync(TUser user);
    Task<TUser?> FindByIdAsync(object id);
    Task<TUser?> FindByLoginAsync(string login);
    Task<bool> HasPermissionAsync(TUser user, string permission);
    Task AddToGroupAsync(TUser user, object groupId);
    Task RemoveFromGroupAsync(TUser user, object groupId);
    Task<IList<TUser>> GetUsersInGroupAsync(object groupId);
    Task<IList<TUser>> GetUsersNotInGroupAsync(object groupId);
    Task<IList<object>> HasPermissionsAsync(TUser user, string permessionKey);
    Task<IList<TPermission>> GetUserPermissionsAsync(TUser user);
}
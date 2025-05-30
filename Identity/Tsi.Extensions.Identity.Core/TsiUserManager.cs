using System.Security.Claims;

namespace Tsi.Extensions.Identity.Core
{
    public class TsiUserManager<TUser,TPermission>
        where TUser : class
        where TPermission : class
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userStore"></param>
        public TsiUserManager(IUserStore<TUser, TPermission> userStore)
        {
            _userStore = userStore;
        }

        protected readonly IUserStore<TUser, TPermission> _userStore;

        /// <summary>
        /// Finding and returns <see cref="Task{User}"/> with given claims principal <paramref name="principal"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>The returns value can be null <see cref="User"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task<TUser?> GetUserAsync(ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);

            var id = GetUserId(principal);
            return id == null ? await Task.FromResult<TUser?>(null) : await FindbyIdAsync(int.Parse(id));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public virtual string? GetUserId(ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        /// /// <exception cref="ArgumentNullException"></exception>
        public virtual string? GetUserName(ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);
            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }
        public virtual async Task<IList<TUser>> GetAsync() {
            return await _userStore.GetAsync();
        }
        public async Task CreateAsync(TUser user)
        {
            await _userStore.CreateAsync(user);
        }
        public async Task UpdateAsync(TUser user)
        {
            await _userStore.UpdateAsync(user);
        }
        public async Task DeleteAsync(TUser user)
        {
            await _userStore.DeleteAsync(user);
        }
        public async Task<TUser?> FindbyIdAsync(object id)
        {
            return await _userStore.FindByIdAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<TUser?> FindByLoginAsync(string login)
        {
            return await _userStore.FindByLoginAsync(login);
        }
        public async Task<bool> HasPermissionAsync(TUser user,string permessionKey)
        {
            return await _userStore.HasPermissionAsync(user,permessionKey);
        }
        public async Task<IList<TUser>> GetUsersNotInGroupAsync(object groupId)
        {
            return await _userStore.GetUsersNotInGroupAsync(groupId);
        }
        public async Task<IList<TUser>> GetUsersInGroupAsync(object groupId)
        {
            return await _userStore.GetUsersInGroupAsync(groupId);
        }

        public async Task AddToGroupAsync(TUser user, object groupId)
        {
            await _userStore.AddToGroupAsync(user, groupId);
        }
        public async Task RemoveFromGroupAsync(TUser user, object groupId)
        {
            await _userStore.RemoveFromGroupAsync(user, groupId);
        }
    }
}

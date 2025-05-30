using System.Linq.Extensions;

namespace Tsi.AspNetCore.Identity.Abstractions
{
    public interface IGroupStore<TGroup, TPermission>
        where TGroup : class
        where TPermission : class
    {

        Task CreateAsync(TGroup group);
        Task UpdateAsync(TGroup group);
        Task DeleteAsync(TGroup group);
        Task<IList<TGroup>> GetAsync();
        Task<IList<TGroup>> GetAsync(Query filter);
        Task<TGroup?> FindByIdAsync(object id);
        Task<TGroup?> FindByNameAsync(string name);
        Task<IList<TPermission>> GetPermissionAsync(TGroup group);
        Task AddPermissionAsync(TGroup group, TPermission permission);
        Task RemovePermissionAsync(TGroup group, TPermission Permission);
    }
}


using Tsi.AspNetCore.Identity.Abstractions;

namespace Tsi.Extensions.Identity.Core
{
    public class TsiGroupManager<TGroup,TPermission>
        where TGroup : class
        where TPermission : class
    {

        public TsiGroupManager(IGroupStore<TGroup, TPermission> groupStore)
        {
            GroupStore= groupStore;
        }

        protected IGroupStore<TGroup, TPermission> GroupStore { get; set; }

        public async Task AddPermissionAsync(TGroup group, TPermission permission) => await GroupStore.AddPermissionAsync(group, permission);

        public async Task CreateAsync(TGroup group) => await GroupStore.CreateAsync(group);

        public async Task DeleteAsync(TGroup group) => await GroupStore.DeleteAsync(group);

        public async Task<TGroup?> FindByIdAsync(object id) => await GroupStore.FindByIdAsync(id);

        public async Task<TGroup?> FindByNameAsync(string name)=>
            await GroupStore.FindByNameAsync(name);

        public async Task<IList<TPermission>> GetPermissionAsync(TGroup group)=>
            await GroupStore.GetPermissionAsync(group);

        public async Task RemovePermissionAsync(TGroup group, TPermission Permission)=>
            await GroupStore.RemovePermissionAsync(group, Permission);

        public async Task UpdateAsync(TGroup group)=>
            await GroupStore.UpdateAsync(group);

        public async Task<IList<TGroup>> GetAsync()=>
            await GroupStore.GetAsync();
    }
}

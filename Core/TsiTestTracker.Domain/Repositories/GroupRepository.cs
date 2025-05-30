

using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class GroupRepository : GenericRepository<Group> , IGroupRepository
    {
        public GroupRepository(IDbContextFactory<TestTrackerContext> dbContextFactory) 
            : base(dbContextFactory)
        {
        }

        public override async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await table.Include(m => m.Permissions)
                .Include(m => m.Users)
                .ToListAsync();
        }
        protected IQueryable<Group> IncludingQueryable()
        {
            return table.Include(m => m.Permissions)
                .Include(m => m.Users)
                ;
        }

        public override void Create(Group entity)
        {
            base.Create(entity);
            var permissions = new List<Permission>()
            {
                new Permission() {Name = "Add user" , Description = "Permission To add Users" , Enable=false,Type = PermissionType.Add_User},
                new Permission() {Name = "Delete user" , Description = "Permission To delete Users" , Enable = true,Type = PermissionType.Delete_User } ,
                new Permission() { Name = "Area_Settings" , Description = "Area_Settings" , Enable = true,Type = PermissionType.Area_Settings}
            };
            entity.Permissions = permissions;
        }

        public void Update(int id , PermissionType permissionType , bool enable)
        {
            var group = table.Include(p => p.Permissions).First(g => g.Id == id);
            if(group is null)
            {
                throw new Exception("");
            }
            var permission = group.Permissions.Where(p => p.Type == permissionType).FirstOrDefault();
            if(permission is not null)
                permission.Enable = enable;
        }

        public override Group GetById(int id)
        {
            return table.Include(g => g.Users)
                .Include(p => p.Permissions).Where(g => g.Id == id).First();

        }
        public void RemoveUser(int idGroup, int idUser)
        {
            var group = table.Include(g => g.Users).First(g => g.Id == idGroup);
            var user = group.Users.Where(u => u.Id == idUser).FirstOrDefault();
            group.Users.Remove(user);
        }

        public async Task<List<User>> GetUsersNotInGroup(int id)
        {
            return await _context.Users
                .Where(u => !u.Groups.Any(g => g.Id == id))
                .ToListAsync();

        }

    }
}

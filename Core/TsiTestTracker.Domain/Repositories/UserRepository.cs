using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories;
public class UserStore : GenericRepository<User>, IUserStore
{
    public UserStore(IDbContextFactory<TestTrackerContext> dbContextFactory) 
        : base(dbContextFactory)
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await table.Include(g => g.Groups)
            .ToListAsync();
    }


   
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
public async Task<IEnumerable<User>> GetUsersNotInGroupAsync(Group group)
    {
        ArgumentNullException.ThrowIfNull(nameof(group));
        var groupId = group.Id;
        var users = await GetAllAsync();
        var usersNotInGrp = new List<User>();   
        foreach(var user in users)
        {
            if (!user.Groups.Select(e=> e.Id).Contains(groupId))
            {
                usersNotInGrp.Add(user);
            }
        }
        return  usersNotInGrp;
    }
    /// <summary>
    /// Finding user by login
    /// </summary>
    /// <param name="login"></param>
    /// <returns><see cref="User" /></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<User?> FindUserByLoginAsync(string login)
    {
        ArgumentNullException.ThrowIfNull(login);

        //return await table.Where(u => u.Login == login)
        //                  .FirstOrDefaultAsync();

        throw new NotImplementedException();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task AddToGroupAsync(User user, string groupName)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task RemoveFromGroupAsync(User user, string groupName)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<Group>> GetGroupAsync(User user)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Permission>> GetPermissionsAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var userId = user.Id;
        var query = from groups in _context.Groups
                    join permissions in _context.Permissions on groups.Id equals permissions.GroupId
                    where groups.Users.Select(u => u.Id).Contains(userId)
                        && permissions.Enable==true
                    select permissions;

        return await query.DistinctBy(prop=>prop.Type)
            .ToListAsync();
    }
}
public interface IUserStore : IGenericRepository<User>
{
    Task<User?> FindUserByLoginAsync(string login);
    Task<IEnumerable<User>> GetUsersNotInGroupAsync(Group group);
    Task AddToGroupAsync(User user, string groupName);
    Task RemoveFromGroupAsync(User user, string groupName);
    Task<IEnumerable<Group>>  GetGroupAsync(User user);
    Task<IEnumerable<Permission>> GetPermissionsAsync(User user);
}

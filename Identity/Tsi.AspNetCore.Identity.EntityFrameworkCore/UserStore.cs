

using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Extensions;
using Tsi.Extensions.Identity.Stores;

namespace Tsi.AspNetCore.Identity.EntityFrameworkCore;

public class UserStore<TUser, TPermission> : UserStore<TUser, DbContext, string, TsiIdentityGroup, TPermission>, IUserStore<TUser, TPermission>
    where TUser : TsiIdentityUser
    where TPermission : TsiIdentityPermission
{

    public UserStore(DbContext context) : base(context)
    {
    }
}

public class UserStore<TUser, TContext, TPermission> : UserStore<TUser, TContext, string, TsiIdentityGroup, TPermission>, IUserStore<TUser, TPermission>
    where TUser : TsiIdentityUser<string>
    where TPermission : TsiIdentityPermission
    where TContext : DbContext
{

    /// <summary>
    /// Create a new instance of <see cref="UserStore{TUser, TContext}"/>
    /// </summary>
    /// <param name="context"></param>
    public UserStore(TContext context) : base(context)
    {
    }
}

/// <summary>
/// Creates a new instance of a persistence store for users.
/// </summary>
/// <typeparam name="TUser"></typeparam>
/// <typeparam name="TContext"></typeparam>
/// <typeparam name="TKey"></typeparam>
public class UserStore<TUser, TContext, TPermission, TKey> : UserStore<TUser, TContext, TKey, TsiIdentityGroup<TKey>, TsiIdentityPermission<TKey>>
    where TUser : TsiIdentityUser<TKey>
    where TContext : DbContext
    where TPermission : TsiIdentityPermission
    where TKey : IEquatable<TKey>
{

    /// <summary>
    /// Create a new instance of <see cref="UserStore{TUser, TContext, TKey}"/>
    /// </summary>
    /// <param name="context"></param>
    public UserStore(TContext context) : base(context)
    {
    }
}

/// <summary>
/// Creates a new instance of a persistence store for users.
/// </summary>
/// <typeparam name="TUser"></typeparam>
/// <typeparam name="TGroup"></typeparam>
/// <typeparam name="TPermission"></typeparam>
/// <typeparam name="TContext"></typeparam>
/// <typeparam name="TKey"></typeparam>
public class UserStore<TUser, TContext, TKey, TGroup, TPermission>
    where TUser : TsiIdentityUser<TKey>
    where TGroup : TsiIdentityGroup<TKey>
    where TPermission : TsiIdentityPermission<TKey>
    where TContext : DbContext
    where TKey : IEquatable<TKey>
{

    private DbSet<TUser> UsersSet { get { return Context.Set<TUser>(); } }
    private DbSet<TGroup> Groups { get { return Context.Set<TGroup>(); } }
    private DbSet<TPermission> Permissions { get { return Context.Set<TPermission>(); } }
    private DbSet<TsiIdentityUserGroup<TKey>> UserGroups { get { return Context.Set<TsiIdentityUserGroup<TKey>>(); } }

    public virtual TContext Context { get; private set; }

    /// <summary>
    /// Constructs a new instance of <see cref="UserStore{TUser, TGroup, TPermission, TContext, TKey}"/>.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/>.</param>
    public UserStore(TContext context)
    {
        Context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool AutoSaveChanges { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected Task SaveChanges()
    {
        return AutoSaveChanges ? Context.SaveChangesAsync() : Task.CompletedTask;
    }

    public virtual async Task<IList<TUser>> GetAsync()
    {
        return await UsersSet.ToListAsync();
    }
    public virtual async Task<IList<TUser>> GetAsync(Query filter)
    {
        return await UsersSet.Where(filter).ToListAsync();
    }

    /// <summary>
    /// Create user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public virtual async Task CreateAsync(TUser user)
    {
        ArgumentNullException.ThrowIfNull(user);
        Context.Add(user);
        await SaveChanges();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public virtual async Task UpdateAsync(TUser user)
    {
        ArgumentNullException.ThrowIfNull(user);
        Context.Attach(user);
        Context.Update(user);
        await SaveChanges();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(TUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        Context.Remove(user);

        await SaveChanges();

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public virtual async Task<TUser?> FindByIdAsync(object id)
    {
        return await UsersSet.FindAsync(id);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public virtual async Task<TUser?> FindByLoginAsync(string login)
    {
        return await UsersSet.FirstOrDefaultAsync(u => u.Login == login);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    public async Task<bool> HasPermissionAsync(TUser user, string permission)
    {
        var query = from userGroup in UserGroups
                    join @groups in Groups on userGroup.GroupId equals @groups.Id
                    join @permissions in Permissions on @groups.Id equals @permissions.GroupId
                    where userGroup.UserId.Equals(user.Id) && @permissions.Name.Equals(permission)
                    select permission;

        return await query.AnyAsync();
    }
    public async Task<IList<object>> HasPermissionsAsync(TUser user, string permission)
    {
        var query = from userGroup in UserGroups
                    join @groups in Groups on userGroup.GroupId equals @groups.Id
                    join @permissions in Permissions on @groups.Id equals @permissions.GroupId
                    where userGroup.UserId.Equals(user.Id) && @permissions.Name.Equals(permission)
                    select permission;

        return (IList<object>)await query.ToListAsync();
    }

    public virtual async Task<IList<TPermission>> GetUserPermissionsAsync(TUser user)
    {
        var query = from userGroup in UserGroups
                    join @groups in Groups on userGroup.GroupId equals @groups.Id
                    join @permissions in Permissions on @groups.Id equals @permissions.GroupId
                    where userGroup.UserId.Equals(user.Id)
                    select @permissions;


        return await query.ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public virtual async Task<IList<TUser>> GetUsersInGroupAsync(object groupId)
    {
        var query = from userGroup in UserGroups
                    join user in UsersSet on userGroup.UserId equals user.Id
                    where userGroup.GroupId.Equals(groupId)
                    select user;

        return await query.ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public virtual async Task<IList<TUser>> GetUsersNotInGroupAsync(object groupId)
    {
        var query =
            from user in UsersSet
            join userGroup in UserGroups.Where(ug => ug.GroupId.Equals(groupId)) on user.Id equals userGroup.UserId
    into joinedGroups
            from userGroup in joinedGroups.DefaultIfEmpty()
            where userGroup == null
            select user;

        return await query.ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public virtual async Task<TGroup?> FindGroupAsync(object groupId)
    {
        return await Groups.FindAsync(groupId);
    }

    /// <summary>
    /// Add user to group with given name groupname
    /// </summary>
    /// <param name="user"></param>
    /// <param name="groupname"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual async Task AddToGroupAsync(TUser user, object groupId)
    {
        var group = await FindGroupAsync(groupId);
        if (group == null)
        {
            throw new InvalidOperationException("group not found" + groupId);
        }
        UserGroups.Add(new TsiIdentityUserGroup<TKey>()
        {
            GroupId = group.Id,
            UserId = user.Id,
        });

        await SaveChanges();
    }
    /// <summary>
    /// Remove user from given group name 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="groupname"></param>
    /// <returns></returns>
    public virtual async Task RemoveFromGroupAsync(TUser user, object groupId)
    {
        var group = await FindGroupAsync(groupId);
        if (group == null)
        {
            throw new InvalidOperationException("group not found" + groupId);
        }
        var userGroup = await UserGroups.FirstOrDefaultAsync(ug => ug.UserId.Equals(user.Id) && ug.GroupId.Equals(groupId));
        if (userGroup != null)
        {
            UserGroups.Remove(userGroup);
        }



        await SaveChanges();
    }
}

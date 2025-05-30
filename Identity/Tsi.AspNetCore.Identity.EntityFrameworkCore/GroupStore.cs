// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using Microsoft.EntityFrameworkCore;
using System.Linq.Extensions;
using Tsi.AspNetCore.Identity.Abstractions;
using Tsi.Extensions.Identity.Stores;

namespace Tsi.AspNetCore.Identity.EntityFrameworkCore;


public class GroupStore : GroupStore<TsiIdentityGroup>
{
    /// <summary>
    /// Constructs a new instance of <see cref="GroupStore"/>.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/>.</param>
    public GroupStore(DbContext context) : base(context) { }
}


public class GroupStore<TGroup> : GroupStore<TGroup, DbContext>
    where TGroup : TsiIdentityGroup
{
    /// <summary>
    /// Constructs a new instance of <see cref="GroupStore{TGroup}"/>.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/>.</param>
    public GroupStore(DbContext context) : base(context) { }
}

public class GroupStore<TGroup, TContext> : GroupStore<TGroup, TContext, string> 
    where TGroup : TsiIdentityGroup
    where TContext : DbContext
{
    /// <summary>
    /// Constructs a new instance of <see cref="GroupStore{TRole, TContext}"/>.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/>.</param>
    public GroupStore(TContext context) : base(context) { }
}

public class GroupStore<TGroup, TContext, TKey> : GroupStore<TGroup, TContext, TKey, TsiIdentityUserGroup<TKey>, TsiIdentityPermission<TKey>>
    where TGroup : TsiIdentityGroup<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    /// <summary>
    /// Constructs a new instance of <see cref="GroupStore{TRole, TContext, TKey}"/>.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/>.</param>
    public GroupStore(TContext context) : base(context) { }
}

/// <summary>
/// Creates a new instance of a persistence store for groups.
/// </summary>
/// <typeparam name="TGroup">The type of the class representing a group.</typeparam>
/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
/// <typeparam name="TKey">The type of the primary key for a group.</typeparam>
/// <typeparam name="TUsergroup">The type of the class representing a user group.</typeparam>
/// <typeparam name="TPermission">The type of the class representing a group Permission.</typeparam>
public class GroupStore<TGroup, TContext, TKey, TUsergroup, TPermission> : IGroupStore<TGroup, TPermission>
    where TGroup : TsiIdentityGroup<TKey>
    where TContext : DbContext
    where TUsergroup : TsiIdentityUserGroup<TKey>, new()
    where TPermission : TsiIdentityPermission<TKey>, new()
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Constructs a new instance of <see cref="GroupStore{TGroup, TContext, TKey, TUsergroup, TPermission}"/>.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/>.</param>
    public GroupStore(TContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        Context = context;
    }

    private bool _disposed;

    /// <summary>
    /// Gets the database context for this store.
    /// </summary>
    public virtual TContext Context { get; private set; }

    /// <summary>
    /// Gets or sets a flag indicating if changes should be persisted after CreateAsync, UpdateAsync and DeleteAsync are called.
    /// </summary>
    /// <value>
    /// True if changes should be automatically persisted, otherwise false.
    /// </value>
    public bool AutoSaveChanges { get; set; } = true;

    /// <summary>Saves the current store.</summary>    
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    protected virtual async Task SaveChanges()
    {
        if (AutoSaveChanges)
        {
            await Context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Creates a new group in a store as an asynchronous operation.
    /// </summary>
    /// <param name="group">The group to create in the store.</param>
    
    /// <returns>A <see cref="Task"/> that represents of the asynchronous query.</returns>
    public virtual async Task CreateAsync(TGroup group)
    { 
        ArgumentNullException.ThrowIfNull(group);
        Context.Add(group);
        await SaveChanges();
    }

    /// <summary>
    /// Updates a group in a store as an asynchronous operation.
    /// </summary>
    /// <param name="group">The group to update in the store.</param>
    
    /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
    public virtual async Task UpdateAsync(TGroup group)
    {
        
        ArgumentNullException.ThrowIfNull(group);
        Context.Attach(group);
        Context.Update(group);
        await SaveChanges();
        
    }

    /// <summary>
    /// Deletes a group from the store as an asynchronous operation.
    /// </summary>
    /// <param name="group">The group to delete from the store.</param>
    
    /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
    public virtual async Task DeleteAsync(TGroup group)
    {
        ArgumentNullException.ThrowIfNull(group);

        Groups.Remove(group);

        await SaveChanges();
    }

    /// <summary>
    /// Finds the group who has the specified ID as an asynchronous operation.
    /// </summary>
    /// <param name="id">The group ID to look for.</param>
    
    /// <returns>A <see cref="Task{TResult}"/> that result of the look up.</returns>
    public virtual async Task<TGroup?> FindByIdAsync(object id)
    {        
        return await Groups.FindAsync(id);
    }

    /// <summary>
    /// Finds the group who has the specified normalized name as an asynchronous operation.
    /// </summary>
    /// <param name="normalizedName">The normalized group name to look for.</param>
    /// <returns>A <see cref="Task{TResult}"/> that result of the look up.</returns>
    public virtual Task<TGroup?> FindByNameAsync(string name)
    {         
        return Groups.FirstOrDefaultAsync(r => r.Name == name);
    }

    /// <summary>
    /// Dispose the stores
    /// </summary>
    public void Dispose() => _disposed = true;

    /// <summary>
    /// Get the permissions associated with the specified <paramref name="group"/> as an asynchronous operation.
    /// </summary>
    /// <param name="group">The group whose permissions should be retrieved.</param>
    /// <returns>A <see cref="Task{TResult}"/> that contains the permissions granted to a group.</returns>
    public virtual async Task<IList<TPermission>> GetPermissionAsync(TGroup group)
    {
        ArgumentNullException.ThrowIfNull(group);

        return await groupPermission
            .Where(rc => rc.GroupId.Equals(group.Id))
            .Select(c => new TPermission()
            {
                Id = c.Id,
                Name = c.Name,
                GroupId= c.GroupId,
                Description= c.Description,
                Key = c.Key,
            }).ToListAsync();
    }

    /// <summary>
    /// Adds the <paramref name="permission"/> given to the specified <paramref name="group"/>.
    /// </summary>
    /// <param name="group">The group to add the Permission to.</param>
    /// <param name="permission">The Permission to add to the group.</param>
    
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public virtual async Task AddPermissionAsync(TGroup group, TPermission permission )
    {
        
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(permission);

        groupPermission.Add(CreateGroupPermission(group, permission));
        await Task.FromResult(false);
    }

    /// <summary>
    /// Removes the <paramref name="Permission"/> given from the specified <paramref name="group"/>.
    /// </summary>
    /// <param name="group">The group to remove the Permission from.</param>
    /// <param name="Permission">The Permission to remove from the group.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public virtual async Task RemovePermissionAsync(TGroup group, TPermission Permission )
    {
        
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(Permission);
        var permissions = await groupPermission.Where(rc => rc.GroupId.Equals(group.Id) && rc.Id.Equals(Permission.Id))
            .ToListAsync();
        foreach (var c in permissions)
        {
            groupPermission.Remove(c);
        }
    }

    /// <summary>
    /// A navigation property for the groups the store contains.
    /// </summary>
    public virtual DbSet<TGroup> Groups => Context.Set<TGroup>();

    private DbSet<TPermission> groupPermission { get { return Context.Set<TPermission>(); } }

    /// <summary>
    /// Creates an entity representing a group Permission.
    /// </summary>
    /// <param name="group">The associated group.</param>
    /// <param name="permission">The associated Permission.</param>
    /// <returns>The group Permission entity.</returns>
    protected virtual TPermission CreateGroupPermission(TGroup group, TPermission permission)
        => new TPermission { GroupId = group.Id, Name = permission.Name, Key = permission.Key ,Description = permission.Description};

    public async Task<IList<TGroup>> GetAsync()
    {
        return await Groups.ToListAsync();
    }

    public async Task<IList<TGroup>> GetAsync(Query filter)
    {
        return await Groups.Where(filter).ToListAsync();
    }
}
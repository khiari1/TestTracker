
using System.Collections.Immutable;
using Tsi.Erp.TestTracker.Infrastructure.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        protected TestTrackerContext _context;

        public ResourceRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();        }
        public async Task<List<KeyValuePair<int, string>>> GetFunctionalities(string? searchPattern) {

            var query = _context.Functionalities as IQueryable<Functionality>;
            if(searchPattern is not null)
            {
                query = query.Where(f => f.Name.Contains(searchPattern));
            }

            return await query
                .Select(f => new KeyValuePair<int, string>(f.Id, f.Name))
                .ToListAsync();

        }

        public async Task<List<KeyValuePair<int, string>>> GetFunctionalitiesBySubMenuId(int subMenuId)=>
            await _context.Functionalities
                 .Where(f => f.SubMenuId == subMenuId)
                .Select(f => new KeyValuePair<int, string>(f.Id, f.Name))
                .ToListAsync();
        public async Task<List<KeyValuePair<int, string>>> GetMenus(string? searchPattern) {
            var query = _context.Menus as IQueryable<Menu>;
            if (searchPattern is not null)
            {
                query = query.Where(f => f.Name.Contains(searchPattern));
            }
            return await query
                .Select(f => new KeyValuePair<int, string>(f.Id, f.Name))
                .ToListAsync();
        }

        public async Task<List<KeyValuePair<int, string>>> GetMenusbyModuleId(int moduleId) {
            var result = await _context.Menus
                .Where(f => f.ModuleId == moduleId)
                .Select(f => new KeyValuePair<int, string>(f.Id, f.Name))
                .ToListAsync();

            return result;
        }
            

        public async Task<List<KeyValuePair<int, string>>> GetModules(string? searchPattern) {
            var query = _context.Modules as IQueryable<Module>;
            if (searchPattern is not null)
            {
                query = query.Where(f => f.Name.Contains(searchPattern));
            }

            return await query
                .Select(f => new KeyValuePair<int, string>(f.Id, f.Name))
                .ToListAsync();
        }


        public async Task<List<KeyValuePair<int, string>>> GetSubMenus(string? searchPattern) {
            var query = _context.SubMenus as IQueryable<SubMenu>;
            if (searchPattern is not null)
            {
                query = query.Where(f => f.Name.Contains(searchPattern));
            }
            return await query
                .Select(f => new KeyValuePair<int, string>(f.Id, f.Name))
                .ToListAsync();
        }

        public async Task<List<KeyValuePair<int, string>>> GetSubMenusByMenuId(int menuId)=>
            await _context.SubMenus
                .Where(f => f.MenuId == menuId)
                .Select(f => new KeyValuePair<int, string>(f.Id, f.Name))
                .ToListAsync();

        public async Task<List<User>> GetUsersAsync(string? serachPattern)
        {
            return await _context.Users                
                .ToListAsync();
        } 
    }
}

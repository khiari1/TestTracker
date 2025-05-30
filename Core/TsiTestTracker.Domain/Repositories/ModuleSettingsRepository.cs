using System;
using System.Drawing;
using Tsi.Erp.TestTracker.Domain.Models;
using Tsi.Erp.TestTracker.Domain.Views;
using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class ModuleSettingsRepository : GenericRepository<Module>, IModuleSettingsRepository
    {
        public ModuleSettingsRepository(IDbContextFactory<TestTrackerContext> dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<IEnumerable<SettingsView>> GetModuleSettings()=>
            await GetModuleSettingsQuery().ToListAsync();

        

        public async Task<IEnumerable<SettingsView>> GetModuleSettings(string searchPattern)
        {
            if (string.IsNullOrEmpty(searchPattern))
            {
                return await GetModuleSettings();
            }
            else
            {
                var query = from module in _context.Modules
                            from menu in _context.Menus.Where(p => module.Id == p.ModuleId).DefaultIfEmpty()
                            from subMenu in _context.SubMenus.Where(p => menu.Id == p.MenuId).DefaultIfEmpty()
                            from func in _context.Functionalities.Where(p => subMenu.Id == p.SubMenuId).DefaultIfEmpty()
                            where menu.Name.Contains(searchPattern) || module.Name.Contains(searchPattern)
                            select new SettingsDetailView()
                            {
                                ModuleId = module.Id,
                                ModuleName = module.Name,
                                MenuId = menu != null ? menu.Id : 0,
                                MenuName = menu != null ? menu.Name : string.Empty,
                                SubMenuId = subMenu != null ? subMenu.Id : 0,
                                SubMenuName = subMenu != null ? subMenu.Name : string.Empty,
                                FunctionalityId = func != null ? func.Id : 0,
                                FunctionalityName = func != null ? func.Name : string.Empty,
                            };

                return await query.GroupBy(q => new { q.ModuleId, q.ModuleName })
                .Select(s => new SettingsView()
                {
                    ModuleId = s.Key.ModuleId,
                    ModuleName = s.Key.ModuleName,
                    SettingsDetails = s.Select(r => new SettingsDetailView()
                    {
                        ModuleId = r.ModuleId,
                        ModuleName = r.ModuleName,
                        MenuId = r != null ? r.MenuId : 0,
                        MenuName = r != null ? r.MenuName : String.Empty,
                        SubMenuId = r != null ? r.SubMenuId : 0,
                        SubMenuName = r != null ? r.SubMenuName : String.Empty,
                        FunctionalityId = r != null ? r.FunctionalityId : 0,
                        FunctionalityName = r != null ? r.FunctionalityName : String.Empty,
                    }).ToList()
                }).ToListAsync();
            }
        }
    
        private IQueryable<SettingsView> GetModuleSettingsQuery()
        {
            var query = from module in _context.Modules
                        from menu in _context.Menus.Where(p => module.Id == p.ModuleId).DefaultIfEmpty()
                        from subMenu in _context.SubMenus.Where(p => menu.Id == p.MenuId).DefaultIfEmpty()
                        from func in _context.Functionalities.Where(p => subMenu.Id == p.SubMenuId).DefaultIfEmpty()
                        select new SettingsDetailView()
                        {
                            ModuleId = module.Id,
                            ModuleName = module.Name,
                            MenuId = menu != null ? menu.Id : 0,
                            MenuName = menu != null ? menu.Name : string.Empty,
                            SubMenuId = subMenu != null ? subMenu.Id : 0,
                            SubMenuName = subMenu != null ? subMenu.Name : string.Empty,
                            FunctionalityId = func != null ? func.Id : 0,
                            FunctionalityName = func != null ? func.Name : string.Empty,
                        };

            return query.GroupBy(q => new { q.ModuleId, q.ModuleName })
                .Select(s => new SettingsView()
                {
                    ModuleId = s.Key.ModuleId,
                    ModuleName = s.Key.ModuleName,
                    SettingsDetails = s.Select(r => new SettingsDetailView()
                    {
                        ModuleId = r.ModuleId,
                        ModuleName = r.ModuleName,
                        MenuId = r != null ? r.MenuId : 0,
                        MenuName = r != null ? r.MenuName : String.Empty,
                        SubMenuId = r != null ? r.SubMenuId : 0,
                        SubMenuName = r != null ? r.SubMenuName : String.Empty,
                        FunctionalityId = r != null ? r.FunctionalityId : 0,
                        FunctionalityName = r != null ? r.FunctionalityName : String.Empty,
                    }).ToList()
                });
        }
    }
}

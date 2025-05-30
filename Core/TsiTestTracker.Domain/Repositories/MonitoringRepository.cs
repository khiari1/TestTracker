using Microsoft.Azure.Pipelines.WebApi;
using Tsi.Erp.TestTracker.Domain.Abstraction;
using Tsi.Erp.TestTracker.Domain.Models;
using Tsi.Erp.TestTracker.Infrastructure.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class MonitoringRepository 
        : GenericRepository<Monitoring>, IMonitoringRepository
    {
        public MonitoringRepository(IDbContextFactory<TestTrackerContext> dbContextFactory) 
            : base(dbContextFactory)
        {
        }
        public override async Task<IEnumerable<Monitoring>> GetAllAsync()
        {
            return await table.Include(m => m.MonitoringDetails)
                .Include(m => m.Functionality)
                .ThenInclude(f => f.SubMenu)
                .ThenInclude(sb => sb.Menu)
                .ThenInclude(m => m.Module)
                .Include(m => m.Tester)
                .ToListAsync();
        }

        public override Monitoring? GetById(int id)
        {
            return table.Include(m => m.MonitoringDetails)
                .Include(m => m.Functionality)
                .ThenInclude(f => f.SubMenu)
                .ThenInclude(sb => sb.Menu)
                .ThenInclude(m => m.Module)
                .Include(m => m.Tester)
                .FirstOrDefault(m => m.Id == id);
        }
        protected IQueryable<Monitoring> IncludingQueryable()
        {
            return table.Include(m => m.MonitoringDetails)
                .Include(m => m.Functionality)
                .ThenInclude(f => f.SubMenu)
                .ThenInclude(sb => sb.Menu)
                .ThenInclude(m => m.Module)
                .Include(m => m.Tester);
        }
        protected IQueryable<MonitoringDetailView> GetDetailQueryable(string? searchPattern, DateTime? date, State? state, int[]? moduleIds = null)
        {
            var queryTest = from monitoring in _context.Monitorings
                            join monitoringDetail in _context.MonitoringDetails on monitoring.Id equals monitoringDetail.MonitoringId
                            join functionality in _context.Functionalities on monitoring.FunctionalityId equals functionality.Id
                            join subMenu in _context.SubMenus on functionality.SubMenuId equals subMenu.Id
                            join menu in _context.Menus on subMenu.MenuId equals menu.Id
                            join module in _context.Modules on menu.ModuleId equals module.Id
                            select new MonitoringDetailView
                            {
                                Id = monitoringDetail.Id,
                                ModuleId = module.Id,
                                MenuId = menu.Id,
                                SubMenuId = subMenu.Id,
                                FunctionalityId = functionality.Id,
                                ModuleName = module.Name,
                                MenuName = menu.Name,
                                SubMenuName = subMenu.Name,
                                FunctionalityName = functionality.Name,
                                State = monitoringDetail.State,
                                ErrorMesage = monitoringDetail.Message,
                                Date = monitoringDetail.Date,
                                BuildVersion = monitoringDetail.BuildVersion,
                                NameMethodeTest = monitoring.NameMethodTest,
                                FailingSince = monitoring.FailingSince,
                                Preconditions=monitoring.Preconditions,
                                UseCase=monitoring.UseCase,
                                AwaitedResult=monitoring.AwaitedResult,
                                Ticket = monitoringDetail.Ticket,
                                StackTrace = monitoringDetail.StackTrace,
                                Duration = monitoringDetail.Duration,

                            };
            if (searchPattern is not null)
            {
                queryTest = queryTest.Where(p => p.MenuName.Contains(searchPattern)
                                  || p.SubMenuName.Contains(searchPattern)
                                  || p.ModuleName.Contains(searchPattern)
                                  || p.FunctionalityName.Contains(searchPattern)
                                  || (p.Ticket != null && p.Ticket.Contains(searchPattern))
                                  || p.NameMethodeTest.Contains(searchPattern)
                                  || (p.UseCase != null && p.UseCase.Contains(searchPattern))                                  
                                  || (p.ErrorMesage != null && p.ErrorMesage.Contains(searchPattern))
                                  
                                 
                                  );
            }
            if (date is not null)
            {
                queryTest = queryTest.Where(md => md.Date.Date == date.Value.Date);
            }
            if (state is not null)
            {
                queryTest = queryTest.Where(md => md.State == state);
            }
            if (moduleIds is not null && moduleIds.Length>0)
            {
                queryTest = queryTest.Where(md => moduleIds.Contains(md.ModuleId));
            }
            return queryTest;
        }
        public async Task<IEnumerable<ModuleSummary>> GetSummaryByModuleAsync(string? searchPattern, DateTime? date, int[]? moduleIds)
        {
            var query = from monitoring in _context.Monitorings
                            join monitoringDetail in _context.MonitoringDetails on monitoring.Id equals monitoringDetail.MonitoringId
                            join functionality in _context.Functionalities on monitoring.FunctionalityId equals functionality.Id
                            join subMenu in _context.SubMenus on functionality.SubMenuId equals subMenu.Id
                            join menu in _context.Menus on subMenu.MenuId equals menu.Id
                            join module in _context.Modules on menu.ModuleId equals module.Id
                            select new ModuleSummary
                            {
                                Id = module.Id,
                                Name = module.Name,
                                Date = monitoringDetail.Date,
                                TotalError = monitoringDetail.State == State.Failed ? 1 : 0,
                                TotalSuccess = monitoringDetail.State == State.Success ? 1 : 0
                            };
            if (searchPattern is not null)
            {
                query = query.Where(p => p.Name.Contains(searchPattern));
            }
            if (moduleIds is not null && moduleIds.Length > 0)
            {
                query = query.Where(md => moduleIds.Contains(md.Id));
            }
            if (date is not null)
            {
                query = query.Where(md => md.Date.Date == date.Value.Date);
            }
            return await query.GroupBy(m => new { m.Id, m.Name }).Select(m => new ModuleSummary
            {
                Id = m.Key.Id,
                Name = m.Key.Name,
                TotalError = m.Sum(ms => ms.TotalError),
                TotalSuccess = m.Sum(ms => ms.TotalSuccess)

            }).ToListAsync();
        }
        public async Task<List<KeyValuePair<int, string>>> GetModules(string? searchPattern)
        {
            var query = _context.Modules as IQueryable<Module>;
            if (searchPattern is not null)
            {
                query = query.Where(f => f.Name.Contains(searchPattern));
            }

            return await query
                .Select(f => new KeyValuePair<int, string>(f.Id, f.Name))
                .ToListAsync();
        }
        public async Task<IEnumerable<MonitoringDetailView>> GetMonitoringDetailsAsync(string? searchPattern, DateTime? date, State? state, int[]? moduleIds = null)
        {
            var queryTest = GetDetailQueryable(searchPattern, date, state, moduleIds);
            return await queryTest.ToListAsync();
        }
        public async Task<MonitoringDetailView?> GetMonitoringDetailAsync(int monitoringDetailId)
        {
            var queryTest = GetDetailQueryable(null, null, null, new int[]{ monitoringDetailId });
            return await queryTest.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Monitoring>> GetMonitoringWithDetailsAsync(string? searchPattern)
        {
            var query = IncludingQueryable();

            if (searchPattern is not null)
            {
                query = query.Where(p => p.NameMethodTest.Contains(searchPattern)
                                  || p.Functionality.SubMenu.Menu.Module.Name.Contains(searchPattern)
                                  || p.Functionality.SubMenu.Menu.Name.Contains(searchPattern)
                                  || p.Functionality.SubMenu.Name.Contains(searchPattern)
                                  || p.Functionality.Name.Contains(searchPattern)
                                  || (p.UseCase != null && p.UseCase.Contains(searchPattern))
                                  || (p.Tester.FirstName != null && p.Tester.FirstName.Contains(searchPattern))
                                  || (p.Tester.LastName != null && p.Tester.LastName.Contains(searchPattern)));



            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<MonitoringDetailView>> GetDetailsByModuleIdAsync(int id, string? searchPattern, DateTime? date, State? state)
        {
            var query = GetDetailQueryable(searchPattern, date, state,new int[] { id });           
           
            return await query.ToListAsync();
        }
       
        public async Task<IEnumerable<MonitoringDetailView>> GetDetailsByMenuIdAsync(int id)
        {
            var query = GetDetailQueryable(null, null, null);

            query = query.Where(query => query.MenuId == id);

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<MonitoringDetailView>> GetDetailsBySubMenuIdAsync(int id)
        {
            var query = GetDetailQueryable(null, null, null);

            query = query.Where(query => query.SubMenuId == id);

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<MonitoringDetailView>> GetDetailsByFunctionalityIdAsync(int id)
        {
            var query = GetDetailQueryable(null, null, null);

            query = query.Where(query => query.FunctionalityId == id);

            return await query.ToListAsync();
        }

        public async Task<int> MonitoringDetailCountAsync(DateTime? date,State? state)
        {
            var query = GetDetailQueryable(null,date: date, state: state);

            return await query.CountAsync();
        }

        public async Task<int> MonitoringDetailCountGroupByModule(DateTime? date, State? state)
        {
            var query = GetDetailQueryable(null, date: date, state: state);

            return await query.CountAsync();
        }
        public async Task<IEnumerable<MonitoringDetailView>> GetDetailAsync(int monitoringDetailId)
        {
            var query = GetDetailQueryable(null, null, null, null);
            query = query.Where(query => query.Id == monitoringDetailId);
            return await query.ToListAsync();
         
        }
        public  IQueryable<MonitoringDetailChart> GetAllMonitoringDetail()
        {
            var queryTest = from monitoring in _context.Monitorings
                            join monitoringDetail in _context.MonitoringDetails on monitoring.Id equals monitoringDetail.MonitoringId
                            join functionality in _context.Functionalities on monitoring.FunctionalityId equals functionality.Id
                            join subMenu in _context.SubMenus on functionality.SubMenuId equals subMenu.Id
                            join menu in _context.Menus on subMenu.MenuId equals menu.Id
                            join module in _context.Modules on menu.ModuleId equals module.Id
                            select new MonitoringDetailChart
                            {
                                Id = monitoringDetail.Id,
                                State = monitoringDetail.State,
                                ModuleName= module.Name,
                                ErrorMesage = monitoringDetail.Message,
                                Date = monitoringDetail.Date,
                               
                            };
            
            
            return queryTest;

        }

        public async Task<IEnumerable<MonitoringDetailChart>> GetAllMonitoringDetailAsync()
        {
            var query = GetAllMonitoringDetail();
            return await query.ToListAsync();
        }
        public async Task<int> GetCountMonitoringByStateAsync(State? state)
        {
            var query = GetAllMonitoringDetail();
            query = query.Where(query => query.State.Equals(state));
            var count = await query.GroupBy(md => md.Date.Day).CountAsync();
            return count;


        }

        
        public async Task<IEnumerable<MonitoringDetailChart>> GetMonitoringByStateAsync(State state)
        {
            var query = GetAllMonitoringDetail();
       
        query = query.Where(query => query.State.Equals(state));
           
            return await query.ToListAsync();
        }


    public async Task DeleteAllMonitoringDetailsAsync()
        {
            var moni = await _context.MonitoringDetails.ToListAsync();

            foreach (var item in moni)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }             
        }

    }

  
}

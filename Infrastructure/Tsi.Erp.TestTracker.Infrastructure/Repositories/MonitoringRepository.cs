using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Domain;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.Domain.Views;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories
{
    public class MonitoringRepository
        : GenericRepository<Monitoring>, IMonitoringRepository
    {
        public MonitoringRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
            : base(dbContextFactory)
        {
        }
        protected IQueryable<MonitoringDetailView> GetDetailQueryable()
        {
            var queryTest = from monitoringDetail in _context.MonitoringDetails
                            join monitoring in _context.Monitorings on monitoringDetail.MonitoringId equals monitoring.Id                       
                            select new MonitoringDetailView
                            {
                                Id = monitoringDetail.Id,                             
                                State = monitoringDetail.Status,
                                ErrorMesage = monitoringDetail.Message,
                                Date = monitoringDetail.Date,
                                BuildVersion = monitoringDetail.BuildVersion,
                                NameMethodeTest = monitoring.NameMethodTest,
                                FailingSince = monitoring.FailingSince,
                                Preconditions = monitoring.Preconditions,
                                UseCase = monitoring.UseCase,
                                AwaitedResult = monitoring.AwaitedResult,
                                Ticket = monitoringDetail.Ticket,
                                StackTrace = monitoringDetail.StackTrace,
                                ExceptionMessage = monitoringDetail.ExceptionMessage,
                                ExceptionType = monitoringDetail.ExceptionType,
                                Duration = monitoringDetail.Duration,
                                ResponcibleName = monitoring.Responsible!.UserName,
                                ResponcibleId = monitoring.Responsible!.Id,
                                TesterName = monitoring.Tester!.UserName,
                                TesterId = monitoring.Tester!.Id,

                            };
            return queryTest;
        }
        protected IQueryable<MonitoringDetailView> GetMonitoringQueryable()
        {
            var queryTest = from monitoring in _context.Monitorings                            
                            select new MonitoringDetailView
                            {
                                Id = monitoring.Id,                              
                                TesterName = monitoring.Tester!.UserName,
                                TesterId = monitoring.Tester!.Id,
                                ResponcibleName = monitoring.Responsible!.UserName,
                                ResponcibleId = monitoring.Responsible!.Id,
                                MonitoringTestType = monitoring.MonitoringTestType,
                                State = monitoring.MonitoringDetails.OrderBy(md => md.Date).FirstOrDefault()!.Status,
                                ErrorMesage = monitoring.MonitoringDetails.OrderBy(md => md.Date).FirstOrDefault()!.Message,
                                Date = monitoring.MonitoringDetails.OrderBy(md => md.Date).FirstOrDefault()!.Date,
                                BuildVersion = monitoring.MonitoringDetails.OrderBy(md => md.Date).FirstOrDefault()!.BuildVersion,
                                NameMethodeTest = monitoring.NameMethodTest,
                                FailingSince = monitoring.FailingSince,
                                Preconditions = monitoring.Preconditions,
                                UseCase = monitoring.UseCase,
                                AwaitedResult = monitoring.AwaitedResult,
                                Ticket = monitoring.MonitoringDetails.OrderBy(md => md.Date).FirstOrDefault()!.Ticket,
                                StackTrace = monitoring.MonitoringDetails.OrderBy(md => md.Date).FirstOrDefault()!.StackTrace,
                                ExceptionMessage = monitoring.MonitoringDetails.OrderBy(md => md.Date).FirstOrDefault()!.ExceptionMessage,
                                ExceptionType = monitoring.MonitoringDetails.OrderBy(md => md.Date).FirstOrDefault()!.ExceptionType,
                                Duration = monitoring.MonitoringDetails.OrderBy(md => md.Date).FirstOrDefault()!.Duration,

                            };

            return queryTest;
        }

        public override async Task<IEnumerable<Monitoring>> GetAsync(Query filter)
        {
            var query = table
                .Where(filter)
                .Include(m => m.MonitoringDetails)             
                .Include(m => m.Tester)
                .Include(m => m.Responsible).AsQueryable();

            return await query
                .ToListAsync();
        }

        public async Task<IEnumerable<MonitoringDetailView>> GetMonitoringsAsync(Query filter)
        {
            var query = GetMonitoringQueryable()
                .AsNoTracking();

            query = query.Where(filter);

            return await query
                .ToListAsync();

        }

        public override Monitoring? Find(int id)
        {
            return table.Include(m => m.MonitoringDetails.OrderByDescending(md => md.Date))              
                .Include(m => m.Tester)
                .Include(m => m.Responsible)
                .FirstOrDefault(m => m.Id == id);
        }

        public async Task DeleteAll()
        {
            var moni = await _context.MonitoringDetails.ToListAsync();

            foreach (var item in moni)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
        }

        public async Task DeleteMonitoringDetailsAsync(int[] ids)
        {
            var monitoringDetails = await _context.MonitoringDetails
                .Where(md => ids.Contains(md.Id))
                .ToListAsync();
            _context.RemoveRange(monitoringDetails);
        }

        public async Task<IEnumerable<MonitoringDetailView>> GetMonitoringDetailAsync(Query filter)
        {
            var query = GetDetailQueryable()
                .AsNoTracking();

            query = query.Where(filter);

            return await query
                .ToListAsync();
        }
    }


}

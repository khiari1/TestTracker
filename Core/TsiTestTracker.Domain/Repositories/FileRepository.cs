

using Tsi.Erp.TestTracker.Infrastructure.Context;
using Attachement = Tsi.Erp.TestTracker.Domain.Models.Attachement;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class FileRepository : GenericRepository<Attachement>,IFileRepository
    {
        public FileRepository(IDbContextFactory<TestTrackerContext> dbContextFactory) 
            : base(dbContextFactory)
        {
        }

        public Attachement? GetFile(string fileName, string keyGroup, int relatedObject)
        {
            return table.Where(f => f.Name == fileName && f.KeyGroup == keyGroup && f.RelatedObject == relatedObject)
                .FirstOrDefault();
        }

        public IEnumerable<Attachement> GetFiles(string keyGroup, int relatedObject)
        {
            return table.Where(f =>f.KeyGroup == keyGroup && f.RelatedObject == relatedObject)
               .ToList();
        }
    }
}

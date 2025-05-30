using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories;
using Attachement = Tsi.Erp.TestTracker.Domain.Stores.Attachement;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories
{
    public class FileRepository : GenericRepository<Attachement>, IFileRepository
    {
        public FileRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
            : base(dbContextFactory)
        {
        }

        public Attachement? GetFile(string fileName, string folder, int objectId)
        {
            return table.Where(f => f.FileName == fileName && f.Folder == folder && f.ObjectId == objectId)
                .FirstOrDefault();
        }

        public IEnumerable<Attachement> GetFiles(string folder, int objectId)
        {
            return table.Where(f => f.Folder == folder && f.ObjectId == objectId)
               .ToList();
        }
    }
}

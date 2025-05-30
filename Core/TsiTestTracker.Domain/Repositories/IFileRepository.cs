using Attachement = Tsi.Erp.TestTracker.Domain.Stores.Attachement;

namespace Tsi.Erp.TestTracker.Domain.Repositories
{
    public interface IFileRepository : IGenericRepository<Attachement>
    {
        Attachement? GetFile(string fileName,string groupKey,int objectId);
        IEnumerable<Attachement> GetFiles(string groupKey, int objectId);
    }
}
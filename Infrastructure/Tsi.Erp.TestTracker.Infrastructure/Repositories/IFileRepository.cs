using System.Collections.Generic;
using Attachement = Tsi.Erp.TestTracker.Domain.Models.Attachement;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public interface IFileRepository : IGenericRepository<Attachement>
    {
        Attachement? GetFile(string fileName,string groupKey,int relatedObject);
        IEnumerable<Attachement> GetFiles(string groupKey, int relatedObject);
    }
}
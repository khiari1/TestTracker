

namespace Tsi.Erp.TestTracker.Domain.Repositories
{
    public interface IResourceRepository
    {
        Task<List<KeyValuePair<int,string>>> GetModules (string? searchPattern);
        Task<List<KeyValuePair<string, string>>> GetUsersAsync(string? searchPattern);

    }
}  
namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public interface IResourceRepository
    {
        Task<List<KeyValuePair<int,string>>> GetModules (string? searchPattern);
        Task<List<KeyValuePair<int, string>>> GetMenus(string? searchPattern);
        Task<List<KeyValuePair<int, string>>> GetMenusbyModuleId(int moduleId);
        Task<List<KeyValuePair<int, string>>> GetSubMenus(string? searchPattern);
        Task<List<KeyValuePair<int, string>>> GetSubMenusByMenuId(int menuId);
        Task<List<KeyValuePair<int, string>>> GetFunctionalities(string? searchPattern);
        Task<List<KeyValuePair<int, string>>> GetFunctionalitiesBySubMenuId(int subMenuId);

        Task<List<User>> GetUsersAsync(string? searchPattern);

    }
}  
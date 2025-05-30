using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        void RemoveUser(int idGroup, int idUser);
        void Update(int id, PermissionType permissionType, bool enable);

        Task<List<User>> GetUsersNotInGroup(int id);
    }
}

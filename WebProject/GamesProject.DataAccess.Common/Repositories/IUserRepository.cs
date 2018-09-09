using GamesProject.DataAccess.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesProject.DataAccess.Common.Repositories
{
    public interface IUserRepository
    {
        Task<DbUser> AddUserAsync(DbUser user);

        Task<DbUser> DeleteUserAsync(DbUser user);

        Task<DbUser> GetUserByIdAsync(int id);

        Task<IEnumerable<DbUser>> GetAllUsersAsync();
    }
}

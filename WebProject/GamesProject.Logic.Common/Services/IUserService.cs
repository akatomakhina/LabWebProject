using GamesProject.Logic.Common.Models;
using System.Threading.Tasks;

namespace GamesProject.Logic.Common.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync();

        Task DeleteUserAsync(int id);
    }
}

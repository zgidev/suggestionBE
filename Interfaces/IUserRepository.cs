using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        // Username
        Task<AppUser> GetUserByUsernameAsync(string name);
        // ID
        Task<AppUser> GetUserByUserIdAsync(int id);
        Task<AppUser> CreateUserAsync(AppUser user);
        Task<bool> UserExist(string username);

    }
}
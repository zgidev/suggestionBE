using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{




    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser> CreateUserAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByUserIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(data => data.Username == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {

            return await _context.Users.ToListAsync();

        }

        public async Task<bool> SaveAllAync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(data => data.Username == username.ToLower());

        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatsterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatsterApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async void Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            _context.SaveChanges();
        }

        public async void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.Include(x => x.Photos).ToListAsync();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(x => x.Photos)
                            .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
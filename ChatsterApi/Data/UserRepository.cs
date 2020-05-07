using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatsterApi.Helpers;
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

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<PagedList<User>> GetAllUsers(UserParams userParams)
        {
            var users = _context.Users.Include(x => x.Photos)
                                      .OrderByDescending(x => x.LastActive)
                                      .AsQueryable();
            users = users.Where(x => x.Id != userParams.UserId);
            users = users.Where(x => x.Gender == userParams.Gender);

            var minDob = DateTime.Now.AddYears(-userParams.MaxAge);
            var maxDob = DateTime.Now.AddYears(-userParams.MinAge);

            users = users.Where(x => x.DateOfBirth >= minDob && x.DateOfBirth <= maxDob);

            switch (userParams.OrderBy.ToLower())
            {
                case "created":
                    users = users.OrderByDescending(x => x.CreatedDate);
                    break;
                default:
                    users = users.OrderByDescending(x => x.LastActive);
                    break;
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Photo> GetMainPhoto(int userId)
        {
            var photo = await _context.Photo.Where(p => p.UserId == userId).FirstOrDefaultAsync(x => x.IsMain);
            return photo;
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
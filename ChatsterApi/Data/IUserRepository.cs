using System.Collections.Generic;
using System.Threading.Tasks;
using ChatsterApi.Helpers;
using ChatsterApi.Models;

namespace ChatsterApi.Data
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<User> GetUser(int id);

        Task<PagedList<User>> GetAllUsers(UserParams userParams);

        Task<bool> SaveAll();
        Task<Photo> GetMainPhoto(int userId);
    }
}
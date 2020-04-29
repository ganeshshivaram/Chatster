using System.Collections.Generic;
using System.Threading.Tasks;
using ChatsterApi.Models;

namespace ChatsterApi.Data
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<User> GetUser(int id);

        Task<List<User>> GetAllUsers();

        Task<bool> SaveAll();
        Task<Photo> GetMainPhoto(int userId);
    }
}
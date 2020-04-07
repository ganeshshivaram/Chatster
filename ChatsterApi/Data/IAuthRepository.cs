using System.Threading.Tasks;
using ChatsterApi.Models;

namespace ChatsterApi.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         
         Task<User> Login(string username, string password);

         Task<bool> DoesUserExist(string username);
    }
}
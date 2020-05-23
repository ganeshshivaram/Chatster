using System.Collections.Generic;
using System.Threading.Tasks;
using ChatsterApi.Helpers;
using ChatsterApi.Models;

namespace ChatsterApi.Data
{
    public interface IMessageRepository
    {
        Task<Message> GetMessage(int id);

        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);

        Task<IEnumerable<Message>> GetMessageThread(int senderId, int receiverId);
    }
}
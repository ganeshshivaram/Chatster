using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatsterApi.Helpers;
using ChatsterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatsterApi.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        public MessageRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<Message> GetMessage(int id)
        {
            return await _context.Message
                            .Include(m => m.Sender).ThenInclude(u => u.Photos)
                            .Include(m => m.Recipient).ThenInclude(u => u.Photos)
                            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Message
                                    .Include(m => m.Sender).ThenInclude(u => u.Photos)
                                    .Include(m => m.Recipient).ThenInclude(u => u.Photos)
                                    .AsQueryable();

            switch (messageParams.MessageContainer.ToLower())
            {
                case "inbox":
                    messages = messages.Where(m => m.RecipientId == messageParams.UserId
                                        && m.RecipientDeleted == false);
                    break;

                case "outbox":
                    messages = messages.Where(m => m.SenderId == messageParams.UserId && m.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(m => m.RecipientId == messageParams.UserId &&
                                        m.RecipientDeleted == false && m.IsRead == false);
                    break;
            }

            messages = messages.OrderByDescending(m => m.MessageSent);

            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int senderId, int receiverId)
        {
            var messages = await _context.Message
                                     .Include(m => m.Sender).ThenInclude(u => u.Photos)
                                     .Include(m => m.Recipient).ThenInclude(u => u.Photos)
                                     .Where(x => (x.SenderId == senderId && x.SenderDeleted == false && x.RecipientId == receiverId) ||
                                            x.RecipientId == senderId && x.RecipientDeleted == false && x.SenderId == receiverId)
                                     .OrderByDescending(m => m.MessageSent)
                                     .ToListAsync();
            return messages;
        }
    }
}
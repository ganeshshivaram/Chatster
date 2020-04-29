using System.Threading.Tasks;
using ChatsterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatsterApi.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;
        public PhotoRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await this._context.Photo.FirstOrDefaultAsync(x => x.Id == id);
            return photo;
        }
    }
}
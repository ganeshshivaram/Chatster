using System.Threading.Tasks;
using ChatsterApi.Models;

public interface IPhotoRepository
{
    Task<Photo> GetPhoto(int id);
}

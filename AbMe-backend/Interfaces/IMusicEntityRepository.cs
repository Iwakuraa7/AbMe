using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Models;

namespace AbMe_backend.Interfaces
{
    public interface IMusicEntityRepository
    {
        Task<List<MusicEntity>> GetMusicEntitiesAsync();
        Task<List<MusicEntity>> GetUserMusicEntitiesAsync(string userId);
        Task<MusicEntity> CreateAsync(MusicEntity musicEntity);
        Task<MusicEntity?> DeleteAsync(int id);
        Task<MusicEntity?> ExistsAsync(int id);
    }
}
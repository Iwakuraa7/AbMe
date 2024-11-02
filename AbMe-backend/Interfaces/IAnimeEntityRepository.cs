using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Models;

namespace AbMe_backend.Interfaces
{
    public interface IAnimeEntityRepository
    {
        public Task<List<AnimeEntity>> GetAnimeListAsync();
        public Task<List<AnimeEntity>> GetUserAnimeListAsync(string userId);
        public Task<AnimeEntity?> ExistsAsync(int animeId);
        public Task<AnimeEntity> CreateAsync(AnimeEntity animeEntity);
        public Task<AnimeEntity> DeleteAsync(AnimeEntity animeEntity);
    }
}
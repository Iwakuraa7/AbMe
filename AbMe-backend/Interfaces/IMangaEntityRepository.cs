using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Models;

namespace AbMe_backend.Interfaces
{
    public interface IMangaEntityRepository
    {
        public Task<List<MangaEntity>> GetMangaListAsync();
        public Task<List<MangaEntity>> GetUserMangaListAsync(string userId);
        public Task<MangaEntity?> ExistsAsync(int mangaId);
        public Task<MangaEntity> CreateAsync(MangaEntity animeEntity);
        public Task<MangaEntity> DeleteAsync(MangaEntity animeEntity);        
    }
}
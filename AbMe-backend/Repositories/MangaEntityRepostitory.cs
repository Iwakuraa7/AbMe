using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Data;
using AbMe_backend.Interfaces;
using AbMe_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace AbMe_backend.Repositories
{
    public class MangaEntityRepostitory : IMangaEntityRepository
    {
        private readonly ApplicationDbContext _context;

        public MangaEntityRepostitory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MangaEntity> CreateAsync(MangaEntity mangaEntity)
        {
            await _context.AddAsync(mangaEntity);
            await _context.SaveChangesAsync();

            return mangaEntity;
        }

        public async Task<MangaEntity> DeleteAsync(MangaEntity mangaEntity)
        {
            _context.Remove(mangaEntity);
            await _context.SaveChangesAsync();

            return mangaEntity;
        }

        public async Task<MangaEntity?> ExistsAsync(int mangaId)
        {
            return await _context.MangaEntities.FirstOrDefaultAsync(a => a.Id == mangaId);
        }

        public async Task<List<MangaEntity>> GetMangaListAsync()
        {
            return await _context.MangaEntities.ToListAsync();
        }

        public async Task<List<MangaEntity>> GetUserMangaListAsync(string userId)
        {
            return await _context.MangaEntities.Where(a => a.AppUserId == userId).ToListAsync();
        }        
    }
}
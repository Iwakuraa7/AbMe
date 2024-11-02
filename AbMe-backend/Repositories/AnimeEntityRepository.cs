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
    public class AnimeEntityRepository : IAnimeEntityRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimeEntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AnimeEntity> CreateAsync(AnimeEntity animeEntity)
        {
            await _context.AddAsync(animeEntity);
            await _context.SaveChangesAsync();

            return animeEntity;
        }

        public async Task<AnimeEntity> DeleteAsync(AnimeEntity animeEntity)
        {
            _context.Remove(animeEntity);
            await _context.SaveChangesAsync();

            return animeEntity;
        }

        public async Task<AnimeEntity?> ExistsAsync(int animeId)
        {
            return await _context.AnimeEntities.FirstOrDefaultAsync(a => a.Id == animeId);
        }

        public async Task<List<AnimeEntity>> GetAnimeListAsync()
        {
            return await _context.AnimeEntities.ToListAsync();
        }

        public async Task<List<AnimeEntity>> GetUserAnimeListAsync(string userId)
        {
            return await _context.AnimeEntities.Where(a => a.AppUserId == userId).ToListAsync();
        }
    }
}
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
    public class MusicEntityRepository : IMusicEntityRepository
    {
        private readonly ApplicationDbContext _context;

        public MusicEntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MusicEntity> CreateAsync(MusicEntity musicEntity)
        {
            await _context.AddAsync(musicEntity);
            await _context.SaveChangesAsync();
            return musicEntity;
        }

        public async Task<List<MusicEntity>> GetMusicEntitiesAsync()
        {
            return await _context.MusicEntities.Include(m => m.AppUser).ToListAsync();
        }

        public async Task<List<MusicEntity>> GetUserMusicEntitiesAsync(string userId)
        {
            return await _context.MusicEntities.Where(m => m.AppUserId == userId).ToListAsync();
        }
    }
}
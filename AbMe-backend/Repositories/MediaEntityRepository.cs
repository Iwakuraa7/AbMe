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
    public class MediaEntityRepository : IMediaEntityRepository
    {
        private readonly ApplicationDbContext _context;

        public MediaEntityRepository(ApplicationDbContext context)
        {
            _context = context;   
        }

        public async Task<MediaEntity> CreateAsync(MediaEntity media)
        {
            await _context.AddAsync(media);
            await _context.SaveChangesAsync();

            return media;
        }

        public async Task<MediaEntity> DeleteAsync(MediaEntity media)
        {
            _context.Remove(media);
            await _context.SaveChangesAsync();

            return media;
        }

        public async Task<MediaEntity?> ExistsAsync(int mediaId)
        {
            var media = await _context.MediaEntities.FirstOrDefaultAsync(m => m.Id == mediaId);

            return media;
        }

        public async Task<List<MediaEntity>> GetMediaEntitiesAsync()
        {
            return await _context.MediaEntities.ToListAsync();
        }

        public async Task<List<MediaEntity>> GetUserMediaEntitiesAsync(string userId)
        {
            return await _context.MediaEntities.Where(m => m.AppUserId == userId).ToListAsync();
        }
    }
}
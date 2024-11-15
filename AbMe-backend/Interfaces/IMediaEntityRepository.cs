using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Models;

namespace AbMe_backend.Interfaces
{
    public interface IMediaEntityRepository
    {
        public Task<List<MediaEntity>> GetMediaEntitiesAsync();
        public Task<List<MediaEntity>> GetUserMediaEntitiesAsync(string userId);
        public Task<MediaEntity?> ExistsAsync(int mediaId);
        public Task<MediaEntity> CreateAsync(MediaEntity media);
        public Task<MediaEntity> DeleteAsync(MediaEntity media);
    }
}
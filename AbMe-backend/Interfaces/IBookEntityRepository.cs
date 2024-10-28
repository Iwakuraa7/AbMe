using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Models;

namespace AbMe_backend.Interfaces
{
    public interface IBookEntityRepository
    {
        public Task<List<BookEntity>> GetBooksAsync();
        public Task<BookEntity?> ExistsAsync(int id);
        public Task<List<BookEntity>> GetUserBooksAsync(string userId);
        public Task<BookEntity> CreateAsync(BookEntity book);
        public Task<BookEntity?> DeleteAsync(BookEntity book);
    }
}
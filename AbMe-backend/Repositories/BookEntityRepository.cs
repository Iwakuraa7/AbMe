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
    public class BookEntityRepository : IBookEntityRepository
    {
        private readonly ApplicationDbContext _context;

        public BookEntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookEntity> CreateAsync(BookEntity book)
        {
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<BookEntity?> DeleteAsync(BookEntity book)
        {
            _context.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<BookEntity?> ExistsAsync(int id)
        {
            var book = await _context.BookEntities.FirstOrDefaultAsync(b => b.Id == id);

            return book;
        }

        public async Task<List<BookEntity>> GetBooksAsync()
        {
            return await _context.BookEntities.ToListAsync();
        }

        public async Task<List<BookEntity>> GetUserBooksAsync(string userId)
        {
            return await _context.BookEntities.Where(b => b.AppUserId == userId).ToListAsync();
        }
    }
}
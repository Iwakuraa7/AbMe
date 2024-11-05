using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Data;
using AbMe_backend.Dtos.UserColor;
using AbMe_backend.Interfaces;
using AbMe_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace AbMe_backend.Repositories
{
    public class UserColorRepository : IUserColorRepository
    {
        private readonly ApplicationDbContext _context;

        public UserColorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserColor> GetUserColorsAsync(string userId)
        {
            return await _context.UserColors.FirstOrDefaultAsync(uc => uc.AppUserId == userId);
        }

        public async Task<UserColor?> UpdateAsync(string userId, CreateUserColorDto userColorDto)
        {
            var colorsToUpdate = await _context.UserColors.FirstOrDefaultAsync(uc => uc.AppUserId == userId);

            if(colorsToUpdate == null)
                return null;

            colorsToUpdate.FirstColor = userColorDto.FirstColor;
            colorsToUpdate.SecondColor = userColorDto.SecondColor;

            await _context.SaveChangesAsync();
            return colorsToUpdate;
        }

        public async Task<UserColor> CreateAsync(UserColor userColor)
        {
            await _context.AddAsync(userColor);
            await _context.SaveChangesAsync();

            return userColor;
        }

        public async Task<UserColor?> ExistsAsync(string userId)
        {
            return await _context.UserColors.FirstOrDefaultAsync(uc => uc.AppUserId == userId);
        }
    }
}
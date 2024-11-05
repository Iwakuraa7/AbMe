using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Dtos.UserColor;
using AbMe_backend.Models;

namespace AbMe_backend.Interfaces
{
    public interface IUserColorRepository
    {
        Task<UserColor> GetUserColorsAsync(string userId);
        Task<UserColor?> UpdateAsync(string userId, CreateUserColorDto userColorDto);
        Task<UserColor> CreateAsync(UserColor userColor);
        Task<UserColor?> ExistsAsync(string userId);
    }
}
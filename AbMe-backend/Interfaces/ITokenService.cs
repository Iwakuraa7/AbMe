using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Models;

namespace AbMe_backend.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
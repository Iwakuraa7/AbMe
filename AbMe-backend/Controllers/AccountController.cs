using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AbMe_backend.Dtos.AppUser;
using AbMe_backend.Interfaces;
using AbMe_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AbMe_backend.Dtos;
using AbMe_backend.Data;
using System.Security.Claims;
using AbMe_backend.Mappers;

namespace AbMe_backend.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMusicEntityRepository _musicEntityRepo;
        private readonly IBookEntityRepository _bookEntityRepo;
        private readonly IAnimeEntityRepository _animeEntityRepo;
        private readonly IMangaEntityRepository _mangaEntityRepo;
        private readonly IUserColorRepository _userColorRepo;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService,
        IMusicEntityRepository musicEntityRepo, IBookEntityRepository bookEntityRepo, IAnimeEntityRepository animeEntityRepo,
        IMangaEntityRepository mangaEntityRepo, IUserColorRepository userColorRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _musicEntityRepo = musicEntityRepo;
            _bookEntityRepo = bookEntityRepo;
            _animeEntityRepo = animeEntityRepo;
            _mangaEntityRepo = mangaEntityRepo;
            _userColorRepo = userColorRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] CreateAppUserDto userDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = userDto.Username,
                    Email = userDto.Email
                };

                var createUserResult = await _userManager.CreateAsync(appUser, userDto.Password);

                if(createUserResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    var defaultUserColor = new UserColor
                    {
                        FirstColor = "#ff6347",
                        SecondColor = "#ffd700",
                        AppUserId = appUser.Id
                    };

                    await _userColorRepo.CreateAsync(defaultUserColor);                     

                    if(roleResult.Succeeded)
                    {
                        var response = new
                        {
                            succeeded =  true,
                            message = "Account successfully created!",
                            token = _tokenService.CreateToken(appUser)
                        };
                        return Ok(response);
                    }
                    else
                    {
                        var response = new
                        {
                            succeeded =  true,
                            message = roleResult.Errors
                        };                        
                        return StatusCode(500, response);
                    }
                }
                else
                {
                    var response = new
                    {
                        succeeded =  true,
                        message = createUserResult.Errors
                    };                      
                    return StatusCode(500, createUserResult.Errors);
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAppUserDto userDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userDto.Username);

                if(user == null)
                {
                    var errorResponse = new 
                    {
                        succeeded = false,
                        message = "User with this username is not found"
                    };

                    return Unauthorized(errorResponse);
                }

                var loginResponse = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

                if(!loginResponse.Succeeded)
                {
                    var errorResponse = new
                    {
                        succeeded = false,
                        message = "Username not found or/and password is incorrect"
                    };

                    return Unauthorized(errorResponse);
                }

                var loggedUser = new NewAppUserDto
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                };
                
                var userColor = await _userColorRepo.ExistsAsync(user.Id);

                if(userColor == null)
                {
                    var defaultUserColor = new UserColor
                    {
                        FirstColor = "#ff6347",
                        SecondColor = "#ffd700",
                        AppUserId = user.Id
                    };

                    await _userColorRepo.CreateAsync(defaultUserColor);                
                }

                var response = new 
                {
                    succeeded = true,
                    message = "Successfully logged in",
                    userInfo = loggedUser
                };

                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("user-hobby-data/{username}")]
        public async Task<IActionResult> GetUserHobbyData([FromRoute] string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
                return NotFound(new {succeeded = false, message = "User not found"});

            var userMusicData = await _musicEntityRepo.GetUserMusicEntitiesAsync(user.Id);
            var userBooksData = await _bookEntityRepo.GetUserBooksAsync(user.Id);
            var userAnimeData = await _animeEntityRepo.GetUserAnimeListAsync(user.Id);
            var userMangaData = await _mangaEntityRepo.GetUserMangaListAsync(user.Id);
            var userColorsData = await _userColorRepo.GetUserColorsAsync(user.Id);

            return Ok(new
            {
                succeeded = true,
                musicData = userMusicData.Select(m => m.fromModelToDto()).OrderByDescending(m => m.Id),
                booksData = userBooksData.Select(b => b.fromModelToDto()).OrderByDescending(m => m.Id),
                animeData = userAnimeData.Select(a => a.fromModelToDto()).OrderByDescending(a => a.Id),
                mangaData = userMangaData.Select(m => m.fromModelToDto()).OrderByDescending(m => m.Id),
                userColors = userColorsData.fromModelToDto()
            });
        }

        [HttpGet("user-search/{username}")]
        public async Task<IActionResult> SearchUserByUsername([FromRoute] string username)
        {
            var users = await _userManager.Users.Where(u => u.UserName.Contains(username)).ToListAsync();
            var resultUsernames = users.Select(u => u.UserName);

            return Ok(new {succeeded = true, usernames = resultUsernames});
        }
    }
}
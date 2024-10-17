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

namespace AbMe_backend.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

                    if(roleResult.Succeeded)
                    {
                        var registeredUser = new NewAppUserDto
                        {
                            Name = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        };

                        var response = new
                        {
                            succeeded =  true,
                            message = "Account successfully created!",
                            userInfo = registeredUser
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
    }
}
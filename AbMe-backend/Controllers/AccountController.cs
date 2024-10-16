using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AbMe_backend.Dtos.AppUser;
using AbMe_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbMe_backend.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                        var response = new
                        {
                            succeeded =  true,
                            message = "Account successfully created!"
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

        [HttpGet("test")]
        public IActionResult test()
        {
            return Ok("Nigga!");
        }
    }
}
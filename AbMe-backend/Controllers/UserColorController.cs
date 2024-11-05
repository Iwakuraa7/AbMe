using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AbMe_backend.Dtos.UserColor;
using AbMe_backend.Interfaces;
using AbMe_backend.Mappers;
using AbMe_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbMe_backend.Controllers
{
    [Route("api/user-color")]
    [ApiController]
    public class UserColorController : ControllerBase
    {
        private readonly IUserColorRepository _userColorRepo;

        public UserColorController(IUserColorRepository userColorRepo)
        {
            _userColorRepo = userColorRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserColor([FromBody] CreateUserColorDto userColorDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(new {succeeded = false, message = "Wrong body"});

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return NotFound(new {succeeded = false, message = "User not found"});                

            var userColorModel = userColorDto.fromCreateToModel();
            userColorModel.AppUserId = userId;

            await _userColorRepo.CreateAsync(userColorModel);

            return Ok(new {succeeded = true, message = "Successfully created user colors"});
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserColor([FromBody] CreateUserColorDto userColorDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return NotFound(new {succeeded = false, message = "User not found"});

            if(!ModelState.IsValid)
                return BadRequest(new {succeeded = false, message = "Wrong body"});

            var userColorToUpdate = await _userColorRepo.UpdateAsync(userId, userColorDto);

            if(userColorToUpdate == null)
                return NotFound(new {succeeded = false, message = "User's colors not found"});

            return Ok(new {succeeded = true, message = "Successfully updated your aura colors"});
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userColor = await _userColorRepo.GetUserColorsAsync(userId);

            return Ok(userColor.fromModelToDto());
        }
    }
}
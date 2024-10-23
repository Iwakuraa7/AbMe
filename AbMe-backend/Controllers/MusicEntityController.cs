using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AbMe_backend.Dtos.MusicEntity;
using AbMe_backend.Interfaces;
using AbMe_backend.Mappers;
using AbMe_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbMe_backend.Controllers
{
    [Route("api/music")]
    [ApiController]
    public class MusicEntityController : ControllerBase
    {
        private readonly IMusicEntityRepository _musicEntityRepo;
        private readonly UserManager<AppUser> _userManager;

        public MusicEntityController(IMusicEntityRepository musicEntityRepo, UserManager<AppUser> userManager)
        {
            _musicEntityRepo = musicEntityRepo;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateMusicEntityDto musicDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(new {succeeded = false, message = "Did not choose music info correctly"});

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return NotFound(new {succeeded = false, message = "User not found"});

            var appUser = await _userManager.FindByIdAsync(userId);

            var musicEntityModel = musicDto.fromMusicDtoToModel();
            musicEntityModel.AppUserId = appUser.Id;
            await _musicEntityRepo.CreateAsync(musicEntityModel);

            return Ok(new {succeeded = true, message = "Successfully created a new musicEntity"});
        }

        [HttpGet]
        public async Task<IActionResult> GetMusicEntities()
        {
            var musicEntities = await _musicEntityRepo.GetMusicEntitiesAsync();

            return Ok(musicEntities.Select(m => m.fromModelToDto()));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserMusicEntities([FromRoute] string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return NotFound(new { succeeded = false, message = "User not found" });
            }

            var musicEntities = await _musicEntityRepo.GetUserMusicEntitiesAsync(user.Id);

            return Ok(new { succeeded = true, musicData = musicEntities.Select(m => m.fromModelToDto()).OrderByDescending(m => m.Id) });
        }
    }
}
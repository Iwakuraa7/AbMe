using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AbMe_backend.Dtos.AnimeEntity;
using AbMe_backend.Interfaces;
using AbMe_backend.Mappers;
using AbMe_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbMe_backend.Controllers
{
    [Route("api/anime")]
    [ApiController]
    public class AnimeEntityController : ControllerBase
    {
        private readonly IAnimeEntityRepository _animeEntityRepo;
        private readonly UserManager<AppUser> _userManager;

        public AnimeEntityController(IAnimeEntityRepository animeEntityRepo, UserManager<AppUser> userManager)
        {
            _animeEntityRepo = animeEntityRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimeList()
        {
            var animeList = await _animeEntityRepo.GetAnimeListAsync();

            return Ok(animeList.Select(a => a.fromModelToDto()));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserAnimeList([FromRoute] string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
                return NotFound(new {succeeded = false, message = "User with the given nickname not found"});

            var animeList = await _animeEntityRepo.GetUserAnimeListAsync(user.Id);

            return Ok(animeList.Select(a => a.fromModelToDto()));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAnimeEntityDto animeDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return BadRequest(new {succeeded = false, message = "User is not logged in"});

            var user = await _userManager.FindByIdAsync(userId);

            if(!ModelState.IsValid)
                return BadRequest(new {succeeded = false, message = "Data inside of the body request is not correct"});

            var animeModel = animeDto.fromCreateDtoToModel();
            animeModel.AppUserId = user.Id;

            await _animeEntityRepo.CreateAsync(animeModel);

            return Ok(new {succeeded = true, message = "Added to your profile"});
        }

        [HttpDelete("delete/{animeId}")]
        public async Task<IActionResult> Delete([FromRoute] int animeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return Unauthorized(new {succeeded = false, message = "User is not authorized"});

            var animeToDelete = await _animeEntityRepo.ExistsAsync(animeId);

            if(animeToDelete == null)
                return NotFound(new {succeeded = false, message = "Anime entity with the given id not found"});

            if(animeToDelete.AppUserId != userId)
                return StatusCode(403, new {succeeded = false, message = "Not allowed to delete other user's info"});                

            await _animeEntityRepo.DeleteAsync(animeToDelete);

            return Ok(new {succeeded = true, message = "Removed from your profile"});
        }
    }
}
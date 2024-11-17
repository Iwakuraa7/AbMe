using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AbMe_backend.Dtos.AnimeEntity;
using AbMe_backend.Dtos.MangaEnitity;
using AbMe_backend.Interfaces;
using AbMe_backend.Mappers;
using AbMe_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AbMe_backend.Controllers
{
    [Route("api/manga")]
    [ApiController]
    public class MangaEntityController : ControllerBase
    {
        private readonly IMangaEntityRepository _mangaEntityRepo;
        private readonly UserManager<AppUser> _userManager;

        public MangaEntityController(IMangaEntityRepository mangaEntityRepo, UserManager<AppUser> userManager)
        {
            _mangaEntityRepo = mangaEntityRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetMangaList()
        {
            var mangaList = await _mangaEntityRepo.GetMangaListAsync();

            return Ok(mangaList.Select(a => a.fromModelToDto()));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserMangaList([FromRoute] string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
                return NotFound(new {succeeded = false, message = "User with the given nickname not found"});

            var mangaList = await _mangaEntityRepo.GetUserMangaListAsync(user.Id);

            return Ok(mangaList.Select(a => a.fromModelToDto()));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateMangaEntityDto mangaDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return BadRequest(new {succeeded = false, message = "User is not logged in"});

            var user = await _userManager.FindByIdAsync(userId);

            if(!ModelState.IsValid)
                return BadRequest(new {succeeded = false, message = "Data inside of the body request is not correct"});

            var mangaModel = mangaDto.fromCreateDtoToModel();
            mangaModel.AppUserId = user.Id;

            await _mangaEntityRepo.CreateAsync(mangaModel);

            return Ok(new {succeeded = true, message = "Successfully added the chosen manga to your profile!"});
        }

        [HttpDelete("delete/{mangaId}")]
        public async Task<IActionResult> Delete([FromRoute] int mangaId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return Unauthorized(new {succeeded = false, message = "User is not authorized"});

            var mangaToDelete = await _mangaEntityRepo.ExistsAsync(mangaId);

            if(mangaToDelete == null)
                return NotFound(new {succeeded = false, message = "Manga entity with the given id not found"});

            if(mangaToDelete.AppUserId != userId)
                return StatusCode(403, new {succeeded = false, message = "Not allowed to delete other user's info"});                

            await _mangaEntityRepo.DeleteAsync(mangaToDelete);

            return Ok(new {succeeded = true, message = "The manga is removed from the profile"});
        }        
    }
}
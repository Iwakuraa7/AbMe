using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AbMe_backend.Dtos.MediaEntity;
using AbMe_backend.Interfaces;
using AbMe_backend.Mappers;
using AbMe_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbMe_backend.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MediaEntityController : ControllerBase
    {
        private readonly IMediaEntityRepository _mediaEntityRepo;
        private readonly UserManager<AppUser> _userManager;

        public MediaEntityController(IMediaEntityRepository mediaEntityRepo, UserManager<AppUser> userManager)
        {
            _mediaEntityRepo = mediaEntityRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetMediaEntities()
        {
            var media = await _mediaEntityRepo.GetMediaEntitiesAsync();

            return Ok(media.Select(m => m.fromModeltoDto()));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserMedia([FromRoute] string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
                return NotFound(new {succeeded = false, message = "User not found"});

            var media = await _mediaEntityRepo.GetUserMediaEntitiesAsync(user.Id);

            return Ok(new {succeeded = true, data = media.Select(m => m.fromModeltoDto())});
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMedia([FromBody] CreateMediaEntityDto mediaDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(new {succeeded = false, message = "Body is not correct"});

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return NotFound(new {succeeded = false, message = "User not found"});

            var mediaModel = mediaDto.fromCreateDtoToModel();
            mediaModel.AppUserId = userId;

            await _mediaEntityRepo.CreateAsync(mediaModel);

            return Ok(new {succeeded = true, message = "Successfully created new media entity"});
        }

        [HttpDelete("delete/{mediaId:int}")]
        public async Task<IActionResult> DeleteMedia([FromRoute] int mediaId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return Unauthorized(new {succeeded = false, message = "User is not logged in"});

            var mediaToDelete = await _mediaEntityRepo.ExistsAsync(mediaId);

            if(mediaToDelete == null)
                return NotFound(new {succeeded = false, message = "Media entity not found"});

            if(mediaToDelete.AppUserId != userId)
                return StatusCode(403, new {succeeded = false, message = "Forbidden to delete media data that is not on your list"});

            await _mediaEntityRepo.DeleteAsync(mediaToDelete);

            return Ok(new {succeeded = true, message = "Successfully deleted media data"});
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AbMe_backend.Dtos.BookEntity;
using AbMe_backend.Interfaces;
using AbMe_backend.Mappers;
using AbMe_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbMe_backend.Controllers
{
    [Route("api/literature")]
    [ApiController]
    public class BookEntityController : ControllerBase
    {
        private readonly IBookEntityRepository _bookEntityRepo;
        private readonly UserManager<AppUser> _userManager;

        public BookEntityController(IBookEntityRepository bookEntityRepo, UserManager<AppUser> userManager)
        {
            _bookEntityRepo = bookEntityRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookEntityRepo.GetBooksAsync();

            return Ok(books.Select(b => b.fromModelToDto()));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserBooks([FromRoute] string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
                return NotFound(new {succeeded = false, message = "User with the nickname does not exist"});
            
            var books = await _bookEntityRepo.GetUserBooksAsync(user.Id);

            return Ok(new {succeeded = true, booksInfo = books.Select(b => b.fromModelToDto())});
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBookEntityDto bookDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(new {succeeded = false, message = "JSON body is not valid"});

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return NotFound(new {succeeded = false, message = "User is not authorized"});

            var bookModel = bookDto.fromCreateDtoToModel();
            bookModel.AppUserId = userId;

            await _bookEntityRepo.CreateAsync(bookModel);

            return Ok(new {succeeded = true, message = "Added! <3"});
        }

        [HttpDelete("delete/{bookId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return Unauthorized(new {succeeded = false, message = "User is not authorized"});

            var book = await _bookEntityRepo.ExistsAsync(bookId);

            if(book == null)
                return NotFound(new {succeeded = false, message = "User does not have the book with the given id"});

            if(book.AppUserId != userId)
                return StatusCode(403, new {succeeded = false, message = "Not allowed to delete other user's info"});

            await _bookEntityRepo.DeleteAsync(book);

            return Ok(new {succeeded = true, message = "Successfully removed from your profile"});
        }
    }
}
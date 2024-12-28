using AbMe_backend.Controllers;
using AbMe_backend.Dtos.AnimeEntity;
using AbMe_backend.Interfaces;
using AbMe_backend.Mappers;
using AbMe_backend.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbMe_backend.Tests.Controllers
{
    public class AnimeEntityControllerTests
    {
        private readonly IAnimeEntityRepository _animeEntityRepo;
        private readonly UserManager<AppUser> _userManager;

        public AnimeEntityControllerTests()
        {
            _animeEntityRepo = A.Fake<IAnimeEntityRepository>();
            _userManager = A.Fake<UserManager<AppUser>>();
        }

        [Fact]
        public async Task AnimeEntityController_GetAnimeList_ReturnsOk()
        {
            // Arrange
            var animeList = new List<AnimeEntity> { new AnimeEntity() };
            A.CallTo(() => _animeEntityRepo.GetAnimeListAsync()).Returns(Task.FromResult(animeList));
            var controller = new AnimeEntityController(_animeEntityRepo, _userManager);

            // Act
            var result = await controller.GetAnimeList();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(animeList.Select(a => a.fromModelToDto()));
        }

        [Fact]
        public async Task AnimeEntityController_Create_ReturnOk()
        {
            // Arrange
            var userId = "userId123";
            var user = new AppUser
            {
                Id = userId,
            };
            var createAnimeDto = new CreateAnimeEntityDto();

            var mockClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));

            var httpContext = new DefaultHttpContext
            {
                User = mockClaimsPrincipal
            };

            var controller = new AnimeEntityController(_animeEntityRepo, _userManager)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };

            A.CallTo(() => _userManager.FindByIdAsync(userId)).Returns(Task.FromResult(user));
            A.CallTo(() => _animeEntityRepo.CreateAsync(createAnimeDto.fromCreateDtoToModel())).Returns(createAnimeDto.fromCreateDtoToModel());

            // Act
            var result = await controller.Create(createAnimeDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(new
            {
                succeeded = true,
                message = "Added to your profile"
            });
        }

        [Fact]
        public async void AnimeEntityController_Delete_ReturnOk()
        {
            // Arrange
            int animeId = 1;
            string userId = "SomeUserId123";
            var animeToDelete = new AnimeEntity
            {
                Id = 1,
                Title = "SomeAnimeTitle",
                ImageUrl = "SomeImgUrl",
                AppUserId = userId
            };

            var mockClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));

            var httpContext = new DefaultHttpContext
            {
                User = mockClaimsPrincipal
            };

            var controller = new AnimeEntityController(_animeEntityRepo, _userManager)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };

            A.CallTo(() => _animeEntityRepo.ExistsAsync(animeId)).Returns(Task.FromResult(animeToDelete));

            // Act
            var result = await controller.Delete(animeId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(new
            {
                succeeded = true,
                message = "Removed from your profile"
            });
        }

        [Fact]
        public async void AnimeEntityController_GetUserAnimeList_ReturnOk()
        {
            // Arrange
            string username = "SomeUsername";
            var user = new AppUser()
            {
                Id = "SomeId"
            };
            var animeList = new List<AnimeEntity> { new AnimeEntity() };

            A.CallTo(() => _userManager.FindByNameAsync(username)).Returns(Task.FromResult(user));
            A.CallTo(() => _animeEntityRepo.GetUserAnimeListAsync(user.Id)).Returns(Task.FromResult(animeList));

            var controller = new AnimeEntityController(_animeEntityRepo, _userManager);

            // Act
            var result = await controller.GetUserAnimeList(username);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okObject = result as OkObjectResult;
            okObject.StatusCode.Should().Be(200);
            okObject.Value.Should().NotBeNull();
            okObject.Value.Should().BeEquivalentTo(animeList.Select(a => a.fromModelToDto()));
        }
    }
}

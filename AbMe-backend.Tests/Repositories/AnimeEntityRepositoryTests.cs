using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbMe_backend.Controllers;
using AbMe_backend.Data;
using AbMe_backend.Interfaces;
using AbMe_backend.Models;
using AbMe_backend.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AbMe_backend.Tests.Repositories
{
    public class AnimeEntityRepositoryTests
    {
        // In-memory db function
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            if(await databaseContext.AnimeEntities.CountAsync() == 0)
            {
                for(int i = 0; i < 10; i++)
                {
                    databaseContext.AnimeEntities.Add(
                        new AnimeEntity()
                        {
                            Title = "Some anime" + i,
                            ImageUrl = "ImgUrl" + i,
                            AppUserId = "UserId123"
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }

            return databaseContext;
        }

        [Fact]
        public async void AnimeEntityRepository_CreateAsync_ReturnAnimeEntity()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var animeEntityRepo = new AnimeEntityRepository(dbContext);
            var animeEntityToCreate = new AnimeEntity
            {
                Id = 100,
                Title = "Title100",
                ImageUrl = "ImgUrl100",
                AppUserId = "UserId123"
            };

            // Act
            var result = await animeEntityRepo.CreateAsync(animeEntityToCreate);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AnimeEntity>();
            result.Should().BeEquivalentTo(animeEntityToCreate, options => options.Excluding(x => x.Id));

            var createdAnimeEntity = await dbContext.AnimeEntities.FirstOrDefaultAsync(a => a.Id == 100);
            createdAnimeEntity.Should().NotBeNull();
            createdAnimeEntity.Should().BeEquivalentTo(animeEntityToCreate);
        }

        [Fact]
        public async void AnimeEntityResitory_DeleteAsync_ReturnAnimeEntity()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var animeEntityRepo = new AnimeEntityRepository(dbContext);
            var animeEntityToDelete = await dbContext.AnimeEntities.FirstOrDefaultAsync(a => a.Id == 1);

            // Act
            var result = await animeEntityRepo.DeleteAsync(animeEntityToDelete);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AnimeEntity>();
            result.Should().BeEquivalentTo(animeEntityToDelete);

            var deletedAnimeEntity = await dbContext.AnimeEntities.FirstOrDefaultAsync(a => a.Id == animeEntityToDelete.Id);
            deletedAnimeEntity.Should().BeNull();
        }

        [Fact]
        public async void AnimeEntityRepository_ExistsAsync_ReturnAnimeEntity()
        {
            // Arrange
            int animeId = 1;
            var dbContext = await GetDbContext();
            var animeEntityRepo = new AnimeEntityRepository(dbContext);

            // Act
            var result = await animeEntityRepo.ExistsAsync(animeId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AnimeEntity>();
            result.Should().BeEquivalentTo(await dbContext.AnimeEntities.FirstOrDefaultAsync(a => a.Id == animeId));
        }

        [Fact]
        public async void AnimeEntityController_GetAnimeListAsync_ReturnListOfAnimeEntities()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var animeEntityRepo = new AnimeEntityRepository(dbContext);

            // Act
            var result = await animeEntityRepo.GetAnimeListAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<AnimeEntity>>();
            result.Should().BeEquivalentTo(await dbContext.AnimeEntities.ToListAsync());
        }

        [Fact]
        public async void AnimeEntityController_GetUserAnimeListAsync_ReturnListOfAnimeEntities()
        {
            // Arrange
            string userId = "UserId123";
            var dbContext = await GetDbContext();
            var animeEntityRepo = new AnimeEntityRepository(dbContext);

            // Act
            var result = await animeEntityRepo.GetUserAnimeListAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<AnimeEntity>>();
            result.Should().BeEquivalentTo(dbContext.AnimeEntities.Where(a => a.AppUserId == userId).ToList());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Dtos.AnimeEntity;
using AbMe_backend.Models;

namespace AbMe_backend.Mappers
{
    public static class AnimeEntityMappers
    {
        public static AnimeEntityDto fromModelToDto(this AnimeEntity animeEntity)
        {
            return new AnimeEntityDto
            {
                Id = animeEntity.Id,
                Title = animeEntity.Title,
                ImageUrl = animeEntity.ImageUrl
            };
        }

        public static AnimeEntity fromCreateDtoToModel(this CreateAnimeEntityDto animeDto)
        {
            return new AnimeEntity
            {
                Title = animeDto.Title,
                ImageUrl = animeDto.ImageUrl
            };
        }
    }
}
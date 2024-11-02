using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Dtos.MangaEnitity;
using AbMe_backend.Models;

namespace AbMe_backend.Mappers
{
    public static class MangaEntityMappers
    {
        public static MangaEntityDto fromModelToDto(this MangaEntity mangaEntity)
        {
            return new MangaEntityDto
            {
                Id = mangaEntity.Id,
                Title = mangaEntity.Title,
                ImageUrl = mangaEntity.ImageUrl
            };
        }

        public static MangaEntity fromCreateDtoToModel(this CreateMangaEntityDto mangaDto)
        {
            return new MangaEntity
            {
                Title = mangaDto.Title,
                ImageUrl = mangaDto.ImageUrl
            };
        }        
    }
}
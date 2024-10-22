using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Dtos.MusicEntity;
using AbMe_backend.Models;

namespace AbMe_backend.Mappers
{
    public static class MusicEntityMappers
    {
        public static MusicEntity fromMusicDtoToModel(this CreateMusicEntityDto musicDto)
        {
            return new MusicEntity
            {
                Title = musicDto.Title,
                ArtistName = musicDto.ArtistName,
                ImageUrl = musicDto.ImageUrl
            };
        }

        public static MusicEntityDto fromModelToDto(this MusicEntity musicEntity)
        {
            return new MusicEntityDto
            {
                Id = musicEntity.Id,
                Title = musicEntity.Title,
                ArtistName = musicEntity.ArtistName,
                ImageUrl = musicEntity.ImageUrl,
                AppUserId = musicEntity.AppUserId
            };
        }
    }
}
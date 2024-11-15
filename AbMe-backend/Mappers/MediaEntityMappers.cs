using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Dtos.MediaEntity;
using AbMe_backend.Models;

namespace AbMe_backend.Mappers
{
    public static class MediaEntityMappers
    {
        public static MediaEntity fromCreateDtoToModel(this CreateMediaEntityDto mediaDto)
        {
            return new MediaEntity
            {
                Title = mediaDto.Title,
                ImageUrl = mediaDto.ImageUrl
            };
        }

        public static MediaEntityDto fromModeltoDto(this MediaEntity mediaEntity)
        {
            return new MediaEntityDto
            {
                Id = mediaEntity.Id,
                Title = mediaEntity.Title,
                ImageUrl = mediaEntity.ImageUrl
            };
        }
    }
}
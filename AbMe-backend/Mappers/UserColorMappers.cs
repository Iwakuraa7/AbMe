using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Dtos.UserColor;
using AbMe_backend.Models;

namespace AbMe_backend.Mappers
{
    public static class UserColorMappers
    {
        public static UserColorDto fromModelToDto(this UserColor userColor)
        {
            return new UserColorDto
            {
                Id = userColor.Id,
                FirstColor = userColor.FirstColor,
                SecondColor = userColor.SecondColor
            };
        }

        public static UserColor fromCreateToModel(this CreateUserColorDto userColorDto)
        {
            return new UserColor
            {
                FirstColor = userColorDto.FirstColor,
                SecondColor = userColorDto.SecondColor
            };
        }
    }
}
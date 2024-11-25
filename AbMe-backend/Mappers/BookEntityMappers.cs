using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbMe_backend.Dtos.BookEntity;
using AbMe_backend.Models;

namespace AbMe_backend.Mappers
{
    public static class BookEntityMappers
    {
        public static BookEntity fromCreateDtoToModel(this CreateBookEntityDto bookDto)
        {
            return new BookEntity
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                ImageUrl = bookDto.ImageUrl
            };
        }

        public static BookEntityDto fromModelToDto(this BookEntity bookModel)
        {
            return new BookEntityDto
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                ImageUrl = bookModel.ImageUrl,
            };
        }
    }
}
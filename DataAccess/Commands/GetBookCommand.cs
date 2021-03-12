using System;
using System.Linq;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using DataAccess.Mapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DataBase;

namespace DataAccess.Commands
{
    public class GetBookCommand : IGetBookCommand
    {
        public OperationResult<BookResponse> Execute(
            [FromServices] IBookResponseMapper responseMapper,
            [FromServices] BooksDbContext booksDbContext,
            int id)
        {
            DbBook book = booksDbContext.Books.FirstOrDefault(b => b.Id == id);
            DbAuthorBook author = booksDbContext.Authors.FirstOrDefault(a => a.BookId == id);
            
            try
            {
                if (book == null)
                {
                    throw new ArgumentException("Incorrect id");
                }

                return new OperationResult<BookResponse>()
                {
                    IsSuccess = true,
                    Body = responseMapper.Map(book, author)
                };
            }
            catch (ArgumentException exc)
            {
                return new OperationResult<BookResponse>()
                {
                    IsSuccess = false,
                    ErrorMessages = {exc.Message}
                };
            }
        }
    }
}
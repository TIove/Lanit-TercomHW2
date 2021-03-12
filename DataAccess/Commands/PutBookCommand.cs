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
    public class PutBookCommand : IPutBookCommand
    {
        public OperationResult<BookResponse> Execute(
            [FromServices] BooksDbContext context,
            [FromServices] IDbBookMapper dbBookMapper,
            [FromServices] IDbAuthorBookMapper dbAuthorBookMapper,
            BookRequest book)
        {
            try
            {
                DbBook oldBook = context.Books.FirstOrDefault(x => x.Id == book.Id);
                DbAuthorBook authorBook = context.Authors.FirstOrDefault(x => x.BookId == book.Id);
                
                if (oldBook == null)
                {
                    throw new ArgumentException("Old model doesn't exist");
                }

                context.Books.Remove(oldBook);
                context.Books.Add(dbBookMapper.Map(book));
                if (authorBook != null)
                    context.Authors.Remove(authorBook);
                
                context.Authors.Add(dbAuthorBookMapper.Map(book));
                context.SaveChanges();

                return new OperationResult<BookResponse>()
                {
                    IsSuccess = true
                };
            }
            catch (Exception exc)
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
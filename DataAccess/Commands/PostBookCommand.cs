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
    public class PostBookCommand : IPostBookCommand
    {
        public OperationResult<BookResponse> Execute(
            [FromServices] BooksDbContext context,
            [FromServices] IDbBookMapper dbBookMapper,
            [FromServices] IDbAuthorBookMapper dbAuthorBookMapper,
            BookRequest book)
        {
            try
            {
                if (context.Books.FirstOrDefault(x => x.Id == book.Id) == null)
                {
                    context.Books.Add(dbBookMapper.Map(book));
                    context.SaveChanges();
                    context.Authors.Add(dbAuthorBookMapper.Map(book));
                    context.SaveChanges();

                    return new OperationResult<BookResponse>()
                    {
                        IsSuccess = true
                    };
                }
                else
                {
                    throw new ArgumentException("This id already exists");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return new OperationResult<BookResponse>()
                {
                    IsSuccess = false,
                    ErrorMessages = {exc.Message}
                };
            }
        }
    }
}
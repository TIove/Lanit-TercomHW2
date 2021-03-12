using System;
using System.Linq;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DataBase;

namespace DataAccess.Commands
{
    public class DeleteBookCommand : IDeleteBookCommand
    {
        public OperationResult<BookResponse> Execute([FromServices] BooksDbContext context, int id)
        {
            try
            {
                DbBook book = context.Books.FirstOrDefault(x => x.Id == id);
                DbAuthorBook authorBook = context.Authors.FirstOrDefault(x => x.BookId == id);
                
                
                if (authorBook != null)
                    context.Authors.Remove(authorBook);
                context.SaveChanges();
                
                if (book == null) 
                    throw new ArgumentException("Model doesn't exist");
                context.Books.Remove(book);
                context.SaveChanges();

                return new OperationResult<BookResponse>()
                {
                    IsSuccess = true
                };
            }
            catch (ArgumentOutOfRangeException exc)
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
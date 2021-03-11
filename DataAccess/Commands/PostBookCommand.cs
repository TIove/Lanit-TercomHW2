using System;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using Models;

namespace DataAccess.Commands
{
    public class PostBookCommand : IPostBookCommand
    {
        public OperationResult<BookResponse> Execute(Book book)
        {
            try
            {
                if (DataBase.Books.Exists(x => x.Id == book?.Id))
                    throw new ArgumentException("This Id already exists");
                DataBase.Books.Add(book);

                return new OperationResult<BookResponse>()
                {
                    IsSuccess = true
                };
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
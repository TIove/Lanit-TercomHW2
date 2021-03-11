using System;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using Models;

namespace DataAccess.Commands
{
    public class PutBookCommand : IPutBookCommand
    {
        public OperationResult<BookResponse> Execute(Book book)
        {
            try
            {
                DataBase.Books[DataBase.Books.FindIndex(x => x.Id == book?.Id)] = book;

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
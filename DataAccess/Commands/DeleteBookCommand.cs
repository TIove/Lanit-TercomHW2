using System;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using Models;

namespace DataAccess.Commands
{
    public class DeleteBookCommand : IDeleteBookCommand
    {
        public OperationResult<BookResponse> Execute(int id)
        {
            try
            {
                DataBase.Books.RemoveAt(DataBase.Books.FindIndex(x => x.Id == id));
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
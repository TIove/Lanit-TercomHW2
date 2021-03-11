using System;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using DataAccess.Mapper;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DataAccess.Commands
{
    public class GetBookCommand : IGetBookCommand
    {
        public OperationResult<BookResponse> Execute(
            [FromServices] IBookMapper mapper,
            int id)
        {
            Book book = DataBase.Books.Find(x => x.Id == id);
            try
            {
                if (book == null)
                    throw new ArgumentException("Incorrect id");

                return new OperationResult<BookResponse>()
                {
                    IsSuccess = true,
                    Body = mapper.Map(book)
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
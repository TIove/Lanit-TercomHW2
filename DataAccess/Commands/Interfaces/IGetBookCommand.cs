using DataAccess.DataBases;
using DataAccess.Mapper;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DataAccess.Commands.Interfaces
{
    public interface IGetBookCommand
    {
        public OperationResult<BookResponse> Execute(
            [FromServices] IBookResponseMapper responseMapper,
            [FromServices] BooksDbContext context,
            int id);
    }
}
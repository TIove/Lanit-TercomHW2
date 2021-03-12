using DataAccess.DataBases;
using DataAccess.Mapper;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DataAccess.Commands.Interfaces
{
    public interface IPostBookCommand
    {
        public OperationResult<BookResponse> Execute(
            [FromServices] BooksDbContext context,
            [FromServices] IDbBookMapper dbBookMapper,
            [FromServices] IDbAuthorBookMapper dbAuthorBookMapper,
            BookRequest book);
    }
}
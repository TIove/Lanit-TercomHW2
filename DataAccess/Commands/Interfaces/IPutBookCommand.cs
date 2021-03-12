using DataAccess.DataBases;
using DataAccess.Mapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DataBase;

namespace DataAccess.Commands.Interfaces
{
    public interface IPutBookCommand
    {
        public OperationResult<BookResponse> Execute(
            [FromServices] BooksDbContext context, 
            [FromServices] IDbBookMapper dbBookMapper,
            [FromServices] IDbAuthorBookMapper dbAuthorBookMapper,
            BookRequest book);
    }
}
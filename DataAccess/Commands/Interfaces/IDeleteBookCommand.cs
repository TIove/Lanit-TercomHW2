using DataAccess.DataBases;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DataAccess.Commands.Interfaces
{
    public interface IDeleteBookCommand
    {
        public OperationResult<BookResponse> Execute([FromServices] BooksDbContext context,int id);
    }
}
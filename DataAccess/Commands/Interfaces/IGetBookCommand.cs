using DataAccess.Mapper;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DataAccess.Commands.Interfaces
{
    public interface IGetBookCommand
    {
        public OperationResult<BookResponse> Execute([FromServices] IBookMapper mapper, int id);
    }
}
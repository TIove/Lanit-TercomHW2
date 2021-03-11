using Models;

namespace DataAccess.Commands.Interfaces
{
    public interface IDeleteBookCommand
    {
        public OperationResult<BookResponse> Execute(int id);
    }
}
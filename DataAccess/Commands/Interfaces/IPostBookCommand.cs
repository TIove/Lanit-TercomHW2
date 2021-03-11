using Models;

namespace DataAccess.Commands.Interfaces
{
    public interface IPostBookCommand
    {
        public OperationResult<BookResponse> Execute(Book book);
    }
}
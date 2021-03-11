using Models;

namespace DataAccess.Commands.Interfaces
{
    public interface IPutBookCommand
    {
        public OperationResult<BookResponse> Execute(Book book);
    }
}
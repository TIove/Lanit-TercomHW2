using DataAccess.Entity;

namespace DataAccess.Commands.Interfaces
{
    public interface IGetBookCommand
    {
        public Book Execute(int id);
    }
}
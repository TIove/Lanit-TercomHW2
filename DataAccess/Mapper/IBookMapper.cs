using Models;

namespace DataAccess.Mapper
{
    public interface IBookMapper
    {
        BookResponse Map(Book book);
    }
}
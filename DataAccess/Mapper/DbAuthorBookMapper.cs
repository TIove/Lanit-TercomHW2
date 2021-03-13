using Models;
using Models.DataBase;
using Models.Enums;

namespace DataAccess.Mapper
{
    public class DbAuthorBookMapper : IDbAuthorBookMapper
    {
        public DbAuthorBook Map(BookRequest book)
        {
            return new DbAuthorBook(book.Id, book.Author);
        }
    }
}
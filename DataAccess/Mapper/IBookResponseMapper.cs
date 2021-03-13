using Models;
using Models.DataBase;

namespace DataAccess.Mapper
{
    public interface IBookResponseMapper
    {
        BookResponse Map(DbBook book, DbAuthorBook authorBook);
    }
}
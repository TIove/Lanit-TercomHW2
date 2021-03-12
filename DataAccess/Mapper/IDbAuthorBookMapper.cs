using Models;
using Models.DataBase;

namespace DataAccess.Mapper
{
    public interface IDbAuthorBookMapper
    {
        DbAuthorBook Map(BookRequest book);
    }
}
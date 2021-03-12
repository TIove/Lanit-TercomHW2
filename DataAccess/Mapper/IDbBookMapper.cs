using Models;
using Models.DataBase;

namespace DataAccess.Mapper
{
    public interface IDbBookMapper
    {
        DbBook Map(BookRequest book);
    }
}
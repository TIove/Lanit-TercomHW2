using Models;
using Models.DataBase;

namespace DataAccess.Mapper
{
    public class DbBookMapper : IDbBookMapper
    {
        public DbBook Map(BookRequest book)
        {
            return new DbBook()
            {
                Id = book.Id,
                Year = book.Year,
                Name = book.Name,
                Description = book.Description
            };
        }
    }
}
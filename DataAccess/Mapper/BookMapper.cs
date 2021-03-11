using System.Diagnostics.CodeAnalysis;
using Models;

namespace DataAccess.Mapper
{
    public class BookMapper : IBookMapper
    {
        public BookResponse Map([NotNull] Book book)
        {
            return new BookResponse()
            {
                Name = book.Name,
                Author = book.Author,
                Year = book.Year,
                Description = book.Description
            };
        }
    }
}
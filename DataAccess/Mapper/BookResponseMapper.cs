using System.Diagnostics.CodeAnalysis;
using Models;
using Models.DataBase;

namespace DataAccess.Mapper
{
    public class BookResponseMapper : IBookResponseMapper
    {
        public BookResponse Map([NotNull] DbBook book, DbAuthorBook authorBook)
        {
            return new BookResponse()
            {
                Name = book.Name,
                Year = book.Year,
                Description = book.Description,
                Author = authorBook.Author
            };
        }
    }
}
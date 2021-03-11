using System.Collections.Generic;
using Models;

namespace DataAccess.DataBases
{
    public static class DataBase
    {
        public static List<Book> Books = new()
        {
            new Book {Id = 1, Name = "nn", Author = "aa", Description = "dd", Year = 123}
        };
    }
}
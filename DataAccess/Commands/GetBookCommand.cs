using System;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using DataAccess.Entity;

namespace DataAccess.Commands
{
    public class GetBookCommand : IGetBookCommand
    {
        public Book Execute(int id)
        {
            Book book = DataBase.Books.Find(x => x.Id == id);
            try
            {
                if (book == null)
                    throw new ArgumentException("Incorrect id");
            }
            catch (ArgumentException exc)
            {
                Console.WriteLine(exc.Message);
            }

            return book;
        }
    }
}
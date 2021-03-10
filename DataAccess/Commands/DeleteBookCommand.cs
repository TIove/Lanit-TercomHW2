using System;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;

namespace DataAccess.Commands
{
    public class DeleteBookCommand : IDeleteBookCommand
    {
        public void Execute(int id)
        {
            try
            {
                DataBase.Books.RemoveAt(DataBase.Books.FindIndex(x => x.Id == id));
            }
            catch (ArgumentOutOfRangeException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
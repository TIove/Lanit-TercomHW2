using System;
using System.Text.Json;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using DataAccess.Entity;

namespace DataAccess.Commands
{
    public class PostBookCommand : IPostBookCommand
    {
        public void Execute(JsonElement bookJson)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            
            try
            {
                Book book = JsonSerializer.Deserialize<Book>(bookJson.ToString()!, options);

                if (DataBase.Books.Exists(x => x.Id == book?.Id))
                    throw new ArgumentException("This Id already exists");
                DataBase.Books.Add(book);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
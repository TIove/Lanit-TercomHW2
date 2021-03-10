using System;
using System.Text.Json;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using DataAccess.Entity;

namespace DataAccess.Commands
{
    public class UpdateBookCommand : IUpdateBookCommand
    {
        public void Execute(JsonElement bookJson)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            try
            {
                var book = JsonSerializer.Deserialize<Book>(bookJson.ToString()!, options);
                DataBase.Books[DataBase.Books.FindIndex(x => x.Id == book?.Id)] = book;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
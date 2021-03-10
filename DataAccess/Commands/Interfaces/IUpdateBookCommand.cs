using System.Text.Json;

namespace DataAccess.Commands.Interfaces
{
    public interface IUpdateBookCommand
    {
        public void Execute(JsonElement bookJson);
    }
}
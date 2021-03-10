using System.Text.Json;

namespace DataAccess.Commands.Interfaces
{
    public interface IPostBookCommand
    {
        public void Execute(JsonElement bookJson);
    }
}
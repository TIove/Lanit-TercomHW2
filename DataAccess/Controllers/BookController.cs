using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DataAccess.Commands.Interfaces;
using DataAccess.Entity;

namespace DataAccess.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        [HttpGet("get")]
        public Book GetBook([FromServices] IGetBookCommand getCommand, [FromQuery] int id)
        {
            return getCommand.Execute(id);
        }
        
        [HttpPost("create")]
        public void CreateBook([FromServices] IPostBookCommand postCommand, [FromBody] JsonElement bookJson)
        {
            postCommand.Execute(bookJson);
        }
        
        [HttpPatch("update")]
        public void UpdateBook([FromServices] IUpdateBookCommand updateCommand, [FromBody] JsonElement bookJson)
        {
            updateCommand.Execute(bookJson);
        }

        [HttpDelete("delete")]
        public void DeleteBook([FromServices] IDeleteBookCommand deleteCommand, [FromQuery] int id)
        {
            deleteCommand.Execute(id);
        }
    }
}
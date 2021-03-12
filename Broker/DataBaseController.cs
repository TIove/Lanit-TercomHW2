using System.Threading.Tasks;
using Broker.Commands;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DataBase;
using Models.Enums;

namespace Broker
{
    [ApiController]
    [Route("[controller]")]
    public class DataBaseController : ControllerBase
    {
        [HttpGet("book/get")]
        public async Task<OperationResult<BookResponse>> GetBook(
            [FromServices] IRequestClient<BookRequest> client,
            [FromServices] IRabbitRequestCommand<BookResponse, BookRequest> command,
            [FromQuery] int id)
        {
            var request = new BookRequest()
            {
                Id = id,
                Mode = RequestMode.Get
            };
            
            return await command.Execute(client, request);
        }

        [HttpPost("book/post")]
        public async Task<OperationResult<BookResponse>> PostBook(
            [FromServices] IRequestClient<BookRequest> client,
            [FromServices] IRabbitRequestCommand<BookResponse, BookRequest> command,
            [FromBody] BookRequest request
        )
        {
            request.Mode = RequestMode.Post;
            return await command.Execute(client, request);
        }

        [HttpPut("book/put")]
        public async Task<OperationResult<BookResponse>> PutBook(
            [FromServices] IRequestClient<BookRequest> client,
            [FromServices] IRabbitRequestCommand<BookResponse, BookRequest> command,
            [FromBody] BookRequest request)
        {
            request.Mode = RequestMode.Put;
            return await command.Execute(client, request);
        }

        [HttpDelete("book/delete")]
        public async Task<OperationResult<BookResponse>> DeleteBook(
            [FromServices] IRequestClient<BookRequest> client,
            [FromServices] IRabbitRequestCommand<BookResponse, BookRequest> command,
            [FromQuery] int id)
        {
            var request = new BookRequest() {Id = id, Mode = RequestMode.Delete};

            return await command.Execute(client, request);
        }
    }
}
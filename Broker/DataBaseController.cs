using System.Threading.Tasks;
using Broker.Commands;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.Enums;

namespace Broker
{
    [ApiController]
    [Route("[controller]")]
    public class DataBaseController : ControllerBase
    {
        private readonly ILogger<DataBaseController> _logger;
        
        public DataBaseController(
            ILogger<DataBaseController> logger)
        {
            _logger = logger;
        }
        
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
            var result = await command.Execute(client, request);
            
            foreach (var it in result.ErrorMessages)
            {
                _logger.Log(LogLevel.Error, it);
            }
            
            return result;
        }

        [HttpPost("book/post")]
        public async Task<OperationResult<BookResponse>> PostBook(
            [FromServices] IRequestClient<BookRequest> client,
            [FromServices] IRabbitRequestCommand<BookResponse, BookRequest> command,
            [FromBody] BookRequest request
        )
        {
            request.Mode = RequestMode.Post;
            var result = await command.Execute(client, request);
            foreach (var it in result.ErrorMessages)
            {
                _logger.Log(LogLevel.Error, it);
            }
            return result;
        }

        [HttpPut("book/put")]
        public async Task<OperationResult<BookResponse>> PutBook(
            [FromServices] IRequestClient<BookRequest> client,
            [FromServices] IRabbitRequestCommand<BookResponse, BookRequest> command,
            [FromBody] BookRequest request)
        {
            request.Mode = RequestMode.Put;
            var result = await command.Execute(client, request);
            foreach (var it in result.ErrorMessages)
            {
                _logger.Log(LogLevel.Error, it);
            }
            return result;
        }

        [HttpDelete("book/delete")]
        public async Task<OperationResult<BookResponse>> DeleteBook(
            [FromServices] IRequestClient<BookRequest> client,
            [FromServices] IRabbitRequestCommand<BookResponse, BookRequest> command,
            [FromQuery] int id)
        {
            var request = new BookRequest() {Id = id, Mode = RequestMode.Delete};
            
            var result = await command.Execute(client, request);
            foreach (var it in result.ErrorMessages)
            {
                _logger.Log(LogLevel.Error, it);
            }
            return result;
        }
    }
}
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Broker
{
    [ApiController]
    [Route("[controller]")]
    public class DataBaseController : ControllerBase
    {
        private static readonly HttpClient BookDbClient = new()
        {
            BaseAddress = new Uri("http://localhost:9000/book/")
        };
        
        [HttpGet("book/get")]
        public async Task<string> GetBook([FromQuery] int id)
        {
            var requestUri = $"get?id={id}";
            return await BookDbClient.GetStringAsync(requestUri);
        }

        [HttpPost("book/create")]
        public async void CreateBook([FromBody] JsonElement strJson)
        {
            const string requestUri = "create";
            await BookDbClient.PostAsJsonAsync(requestUri, strJson);
        }
        
        [HttpPatch("book/update")]
        public async void UpdateBook([FromBody] JsonElement strJson)
        {
            const string requestUri = "update";
            await BookDbClient.PatchAsync(requestUri, JsonContent.Create(strJson));
        }
        
        [HttpDelete("book/delete")]
        public async void DeleteBook([FromQuery] int id)
        {
            string requestUri = $"delete?id={id}";
            await BookDbClient.DeleteAsync(requestUri);
        } 
    }
}
using System.Threading.Tasks;
using MassTransit;
using Models;

namespace Broker.Commands
{
    public class RabbitRequestCommand<TResponse, TRequest> : IRabbitRequestCommand<TResponse, TRequest>
        where TRequest : class
    {
        public async Task<OperationResult<TResponse>> Execute(
            IRequestClient<TRequest> client,
            TRequest request)
        {
            var response = await client.GetResponse<OperationResult<TResponse>>(request);
            return response.Message;
        }
    }
}
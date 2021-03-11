using System.Threading.Tasks;
using MassTransit;
using Models;

namespace Broker.Commands
{
    public interface IRabbitRequestCommand<TResponse, TRequest> where TRequest : class
    {
        public Task<OperationResult<TResponse>> Execute(
            IRequestClient<TRequest> client,
            TRequest request
        );
    }
}
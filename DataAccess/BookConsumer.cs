using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Commands;
using DataAccess.Commands.Interfaces;
using DataAccess.Mapper;
using MassTransit;
using MassTransit.Futures.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enums;

namespace DataAccess
{
    public class BookConsumer : IConsumer<BookRequest>
    {
        private readonly IGetBookCommand _getBookCommand;
        private readonly IPostBookCommand _postBookCommand;
        private readonly IPutBookCommand _putBookCommand;
        private readonly IDeleteBookCommand _deleteBookCommand;
        private readonly IBookMapper _bookMapper;

        public BookConsumer(
            [FromServices] IGetBookCommand getBookCommand,
            [FromServices] IPostBookCommand postBookCommand,
            [FromServices] IPutBookCommand putBookCommand,
            [FromServices] IDeleteBookCommand deleteBookCommand,
            IBookMapper bookMapper)
        {
            _getBookCommand = getBookCommand;
            _postBookCommand = postBookCommand;
            _putBookCommand = putBookCommand;
            _deleteBookCommand = deleteBookCommand;
            _bookMapper = bookMapper;
        }

        public async Task Consume(ConsumeContext<BookRequest> context)
        {
            var operationResult = new OperationResult<BookResponse>();
            try
            {
                operationResult = context.Message.Mode switch
                {
                    RequestMode.Get => _getBookCommand.Execute(_bookMapper, context.Message.Id),
                    RequestMode.Post => _postBookCommand.Execute(context.Message.ModelBody),
                    RequestMode.Put => _putBookCommand.Execute(context.Message.ModelBody),
                    RequestMode.Delete => _deleteBookCommand.Execute(context.Message.Id),
                    _ => throw new ArgumentException("Mode of book consumer wasn't installed")
                };
            }
            catch (Exception exc)
            {
                operationResult.IsSuccess = false;
                operationResult.ErrorMessages = new List<string> {exc.Message};
            }

            await context.RespondAsync(operationResult);
        }
    }
}
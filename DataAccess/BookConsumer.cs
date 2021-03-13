using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Commands.Interfaces;
using MassTransit;
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
        public BookConsumer(
            [FromServices] IGetBookCommand getBookCommand,
            [FromServices] IPostBookCommand postBookCommand,
            [FromServices] IPutBookCommand putBookCommand,
            [FromServices] IDeleteBookCommand deleteBookCommand)
        {
            _getBookCommand = getBookCommand;
            _postBookCommand = postBookCommand;
            _putBookCommand = putBookCommand;
            _deleteBookCommand = deleteBookCommand;
        }

        public async Task Consume(ConsumeContext<BookRequest> context)
        {
            var operationResult = new OperationResult<BookResponse>();
            try
            {
                operationResult = context.Message.Mode switch
                {
                    RequestMode.Get =>
                        _getBookCommand.Execute(context.Message.Id),
                    RequestMode.Post => 
                        _postBookCommand.Execute(context.Message),
                    RequestMode.Put => 
                        _putBookCommand.Execute(context.Message),
                    RequestMode.Delete => 
                        _deleteBookCommand.Execute(context.Message.Id),
                    _ 
                        => throw new ArgumentException("Mode of book consumer wasn't installed")
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
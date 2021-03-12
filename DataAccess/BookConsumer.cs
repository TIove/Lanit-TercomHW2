using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using DataAccess.Mapper;
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
        private readonly IBookResponseMapper _bookResponseMapper;
        private readonly BooksDbContext _booksDbContext;
        private readonly IDbBookMapper _dbBookMapper;
        private readonly IDbAuthorBookMapper _dbAuthorBookMapper;
        
        public BookConsumer(
            [FromServices] IGetBookCommand getBookCommand,
            [FromServices] IPostBookCommand postBookCommand,
            [FromServices] IPutBookCommand putBookCommand,
            [FromServices] IDeleteBookCommand deleteBookCommand,
            [FromServices] BooksDbContext booksDbContext,
            [FromServices] IDbBookMapper dbBookMapper,
            [FromServices] IDbAuthorBookMapper dbAuthorBookMapper,
            IBookResponseMapper bookResponseMapper)
        {
            _getBookCommand = getBookCommand;
            _postBookCommand = postBookCommand;
            _putBookCommand = putBookCommand;
            _deleteBookCommand = deleteBookCommand;
            _bookResponseMapper = bookResponseMapper;
            _booksDbContext = booksDbContext;
            _dbBookMapper = dbBookMapper;
            _dbAuthorBookMapper = dbAuthorBookMapper;
        }

        public async Task Consume(ConsumeContext<BookRequest> context)
        {
            var operationResult = new OperationResult<BookResponse>();
            try
            {
                operationResult = context.Message.Mode switch
                {
                    RequestMode.Get => 
                        _getBookCommand.Execute(_bookResponseMapper, _booksDbContext, context.Message.Id),
                    RequestMode.Post => 
                        _postBookCommand.Execute(_booksDbContext, _dbBookMapper, _dbAuthorBookMapper, context.Message),
                    RequestMode.Put => 
                        _putBookCommand.Execute(_booksDbContext, _dbBookMapper, _dbAuthorBookMapper, context.Message),
                    RequestMode.Delete => 
                        _deleteBookCommand.Execute(_booksDbContext, context.Message.Id),
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
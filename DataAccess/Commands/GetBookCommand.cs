using System;
using System.Linq;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using DataAccess.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DataBase;

namespace DataAccess.Commands
{
    public class GetBookCommand : IGetBookCommand
    {
        private IBookResponseMapper _responseMapper;
        private BooksDbContext _bookDbContext;
        public GetBookCommand(
            [FromServices] IBookResponseMapper responseMapper,
            [FromServices] BooksDbContext booksDbContext)
        {
            _responseMapper = responseMapper;
            _bookDbContext = booksDbContext;
        }
        public OperationResult<BookResponse> Execute(int id)
        {
            DbBook book = _bookDbContext.Books.FirstOrDefault(b => b.Id == id);
            DbAuthorBook author = _bookDbContext.Authors.FirstOrDefault(a => a.BookId == id);
            
            try
            {
                if (book == null)
                {
                    throw new ArgumentException("Incorrect id");
                }

                return new OperationResult<BookResponse>()
                {
                    IsSuccess = true,
                    Body = _responseMapper.Map(book, author)
                };
            }
            catch (ArgumentException exc)
            {
                return new OperationResult<BookResponse>()
                {
                    IsSuccess = false,
                    ErrorMessages = {exc.Message}
                };
            }
        }
    }
}
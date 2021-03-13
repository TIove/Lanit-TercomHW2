using System;
using System.Linq;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using DataAccess.Mapper;
using MassTransit.Initializers.Variables;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DataBase;

namespace DataAccess.Commands
{
    public class PostBookCommand : IPostBookCommand
    {
        private BooksDbContext _context;
        private IDbBookMapper _bookMapper;
        private IDbAuthorBookMapper _authorBookMapper;
        public PostBookCommand(
            [FromServices] BooksDbContext context,
            [FromServices] IDbBookMapper dbBookMapper,
            [FromServices] IDbAuthorBookMapper dbAuthorBookMapper)
        {
            _context = context;
            _bookMapper = dbBookMapper;
            _authorBookMapper = dbAuthorBookMapper;
        }
        public OperationResult<BookResponse> Execute(BookRequest book)
        {
            try
            {
                if (_context.Books.FirstOrDefault(x => x.Id == book.Id) == null)
                {
                    _context.Books.Add(_bookMapper.Map(book));
                    _context.SaveChanges();
                    _context.Authors.Add(_authorBookMapper.Map(book));
                    _context.SaveChanges();

                    return new OperationResult<BookResponse>()
                    {
                        IsSuccess = true
                    };
                }
                else
                {
                    throw new ArgumentException("This id already exists");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return new OperationResult<BookResponse>()
                {
                    IsSuccess = false,
                    ErrorMessages = {exc.Message}
                };
            }
        }
    }
}
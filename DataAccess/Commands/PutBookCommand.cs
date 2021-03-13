using System;
using System.Linq;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using DataAccess.Mapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DataBase;

namespace DataAccess.Commands
{
    public class PutBookCommand : IPutBookCommand
    {
        private BooksDbContext _context;
        private IDbBookMapper _dbBookMapper;
        private IDbAuthorBookMapper _dbAuthorBookMapper;
        public PutBookCommand(
            [FromServices] BooksDbContext context,
            [FromServices] IDbBookMapper dbBookMapper,
            [FromServices] IDbAuthorBookMapper dbAuthorBookMapper)
        {
            _context = context;
            _dbBookMapper = dbBookMapper;
            _dbAuthorBookMapper = dbAuthorBookMapper;
        }
        
        public OperationResult<BookResponse> Execute(
            BookRequest book)
        {
            try
            {
                DbBook oldBook = _context.Books.FirstOrDefault(x => x.Id == book.Id);
                DbAuthorBook authorBook = _context.Authors.FirstOrDefault(x => x.BookId == book.Id);
                
                if (oldBook == null)
                {
                    throw new ArgumentException("Old model doesn't exist");
                }

                _context.Books.Remove(oldBook);
                _context.Books.Add(_dbBookMapper.Map(book));
                if (authorBook != null)
                    _context.Authors.Remove(authorBook);
                
                _context.Authors.Add(_dbAuthorBookMapper.Map(book));
                _context.SaveChanges();

                return new OperationResult<BookResponse>()
                {
                    IsSuccess = true
                };
            }
            catch (Exception exc)
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
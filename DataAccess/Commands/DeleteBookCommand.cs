using System;
using System.Linq;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DataBase;

namespace DataAccess.Commands
{
    public class DeleteBookCommand : IDeleteBookCommand
    {
        private BooksDbContext _context;
        public DeleteBookCommand([FromServices] BooksDbContext context)
        {
            _context = context;
        }
        public OperationResult<BookResponse> Execute(int id)
        {
            try
            {
                DbBook book = _context.Books.FirstOrDefault(x => x.Id == id);
                DbAuthorBook authorBook = _context.Authors.FirstOrDefault(x => x.BookId == id);
                
                
                if (authorBook != null)
                    _context.Authors.Remove(authorBook);
                _context.SaveChanges();
                
                if (book == null) 
                    throw new ArgumentException("Model doesn't exist");
                _context.Books.Remove(book);
                _context.SaveChanges();

                return new OperationResult<BookResponse>()
                {
                    IsSuccess = true
                };
            }
            catch (ArgumentOutOfRangeException exc)
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
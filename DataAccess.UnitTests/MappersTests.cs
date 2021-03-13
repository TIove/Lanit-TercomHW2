using DataAccess.Mapper;
using Models;
using Models.DataBase;
using NUnit.Framework;

namespace DataAccess.UnitTests
{
    public class Tests
    {
        private BookResponseMapper _responseMapper;
        private DbBookMapper _dbBookMapper;
        private DbAuthorBookMapper _dbAuthorBookMapper;

        private BookRequest _bookRequest;
        private DbBook _dbBook;
        private BookResponse _bookResponse;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _responseMapper = new BookResponseMapper();
            _dbBookMapper = new DbBookMapper();
            _dbAuthorBookMapper = new DbAuthorBookMapper();
        }

        [SetUp]
        public void SetUp()
        {
            _bookRequest = new BookRequest()
            {
                Id = 1,
                Year = 123,
                Name = "af"
            };
            
            _dbBook = new DbBook()
            {
                Id = 1,
                Year = 123,
                Name = "af"
            };
            
            _bookResponse = new BookResponse()
            {
                Year = 123,
                Name = "af"
            };
        }
        
        [Test]
        public void BookResponseMapperTest()
        {
            var response = _responseMapper.Map(_dbBook, null);
            Assert.AreEqual(_bookResponse.Author, response.Author);
            Assert.AreEqual(_bookResponse.Description, response.Description);
            Assert.AreEqual(_bookResponse.Name, response.Name);
            Assert.AreEqual(_bookResponse.Year, response.Year);
        }

        [Test]
        public void DbBookMapperTest()
        {
            var response = _dbBookMapper.Map(_bookRequest);
            Assert.AreEqual(_dbBook.Id, response.Id);
            Assert.AreEqual(_dbBook.Description, response.Description);
            Assert.AreEqual(_dbBook.Name, response.Name);
            Assert.AreEqual(_dbBook.Year, response.Year);
        }

        [Test]
        public void DbAuthorBookMapperTest()
        {
            _bookRequest.Author = "abc";
            var response = _dbAuthorBookMapper.Map(_bookRequest);
            var dbAuthor = new DbAuthorBook(1, "abc");
            Assert.AreEqual(dbAuthor.Author, response.Author);
        }
    }
}
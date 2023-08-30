using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;      
        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;            
        }

        [Fact]
        public void WhenExistBookIdIsNotGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arange                              
            DeleteBookCommand command = new DeleteBookCommand(_context);            
            command.BookId = 999;
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);

            // act & assert        
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadi");           
                
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
        {
            // arrange           
            DeleteBookCommand command = new (_context);
            command.BookId = 1;
                     

            // act & assert
            FluentActions
            .Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().BeNull();                     
        }
    }
}
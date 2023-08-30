using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Commands.UpdatedBook;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.UpdatedBook
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;             
        public UpdateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;            
        }

        [Fact]
        public void WhenExistBookIdIsNotGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arange                              
            UpdatedBookCommand command = new UpdatedBookCommand(_context);            
            command.BookId = 999;
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);

            // act & assert            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadi");           
        
           
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            // arrange            
            UpdatedBookCommand command = new (_context);
            command.BookId = 2;
            UpdatedBookModel model = new() { Title = "Harry Potter", GenreId = 1, AuthorId = 1};
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke(); 

            // assert
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().NotBeNull();
            book.Title.Should().Be(model.Title);        
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);         
        } 
    }
}
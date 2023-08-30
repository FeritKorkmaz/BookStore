using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]

        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {                   
            //arrange
            var book = new Book { 
                Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", 
                PageCount = 100, 
                PublishDate = new DateTime(2000, 1, 1), 
                GenreId = 1, 
                AuthorId = 1};

            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel { Title = book.Title };
 
            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut"); 
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            // arrange
            CreateBookCommand command = new (_context, _mapper);
            CreateBookModel model = new() { Title = "Lord Of The Rings", GenreId = 1, PageCount = 100, PublishDate = new DateTime(2000, 1, 1), AuthorId = 1};
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke(); 

            // assert
            var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);           
        } 
    }
}
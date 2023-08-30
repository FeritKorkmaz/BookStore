using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;      
        public DeleteAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context; 
            _mapper = testFixture.Mapper;                      
        }

        [Fact]
        public void WhenExistAuthorIdIsNotGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arange                              
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);            
            command.AuthorId = 999;
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);

            // act & assert        
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadi");           
                
        }

        [Fact]
        public void When_InvalidOperationException_ShouldBeReturn_IfTheExistingAuthorId_BelongsToBook()
        {
            // arrange           
            DeleteAuthorCommand command = new (_context);
            command.AuthorId = 1;
            var book = _context.Books.Any(book => book.AuthorId == command.AuthorId);
                        

            // act & assert
            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu yazarin yayinda bir kitabi var. Lütfen önce kitabi siliniz.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
        {
            // arrange
            CreateAuthorCommand createCommand = new (_context, _mapper);
            CreateAuthorModel model = new() { Name = "Test" , Surname = "Test" , DateOfBirth = DateTime.Parse("1990-01-01") };
            createCommand.Model = model;

            DeleteAuthorCommand command = new (_context);
            command.AuthorId = 4;

            // act
            FluentActions.Invoking(() => createCommand.Handle()).Invoke();
            FluentActions.Invoking(() => command.Handle()).Invoke(); 

            // assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);
            author.Should().BeNull();                                                   
        }
    }

}
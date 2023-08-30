using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;      
        public DeleteGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;            
        }

        [Fact]
        public void WhenExistGenreIdIsNotGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arange                              
            DeleteGenreCommand command = new DeleteGenreCommand(_context);            
            command.GenreId = 999;
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);

            // act & assert        
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadi");           
                
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {
            // arrange           
            DeleteGenreCommand command = new (_context);
            command.GenreId = 1;
                        

            // act & assert
            FluentActions
            .Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(book => book.Id == command.GenreId);
            genre.Should().BeNull();                     
        }
    }

}
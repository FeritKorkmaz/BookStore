using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;             
        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;            
        }

        [Fact]
        public void WhenExistGenreIdIsNotGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arange                              
            UpdateGenreCommand command = new UpdateGenreCommand(_context);            
            command.GenreId = 999;
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);

            // act & assert            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadi");                 
        }

        [Fact]
        public void WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange            
            UpdateGenreCommand command = new (_context);
            command.GenreId = 2;
            UpdateGenreModel model = new() { Name = "Romence", IsActive = true };
            command.Model = model;           

            // act & assert            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Ayni isimli bir kitap türü zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            // arrange            
            UpdateGenreCommand command = new (_context);
            command.GenreId = 2;
            UpdateGenreModel model = new() { Name = "Fear", IsActive = true };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke(); 

            // assert
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            genre.IsActive.Should().Be(model.IsActive);
        } 
    }

}
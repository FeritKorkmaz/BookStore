using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;       

        public CreateGenreCommandTest(CommonTestFixture testFixture)
        {
            
            _context = testFixture.Context;
            
        }

        [Fact]
        public void WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {                   
            //arrange
            var genre = new Genre { 
                Name = "Test_WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn", 
              };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel { Name = genre.Name };
 
            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut"); 
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // arrange
            CreateGenreCommand command = new (_context);
            CreateGenreModel model = new() { Name = "Test"};
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke(); 

            // assert
            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);
            genre.Should().NotBeNull();                     
        } 
    }
}
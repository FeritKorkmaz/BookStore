using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;             
        public UpdateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;            
        }

        [Fact]
        public void WhenExistAuthorIdIsNotGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arange                              
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);            
            command.AuthorId = 999;
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);

            // act & assert            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadi");                 
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameAndSurnameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange            
            UpdateAuthorCommand command = new (_context);
            command.AuthorId = 2;
            UpdateAuthorModel model = new() { Name = "John", Surname = "Doe", DateOfBirth = DateTime.Parse("1990-01-01") };
            command.Model = model;
                 

            // act & assert            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Ayni isim soyisimli bir yazar zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            // arrange            
            UpdateAuthorCommand command = new (_context);
            command.AuthorId = 2;
            UpdateAuthorModel model = new() { Name = "Ferit", Surname = "Korkmaz", DateOfBirth = DateTime.Parse("1990-01-01") };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke(); 

            // assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.DateOfBirth.Should().Be(model.DateOfBirth);
        } 
    }

}
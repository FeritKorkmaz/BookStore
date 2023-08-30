using System.Runtime.Serialization;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context; 
        private readonly IMapper _mapper;      

        public CreateAuthorCommandTest(CommonTestFixture testFixture)
        {
            
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameAndSurnameIsGiven_InvalidOperationException_ShouldBeReturn()
        {                   
            //arrange
            var author = new Author { 
                Name = "Test_WhenAlreadyExistAuthorNameAndSurnameIsGiven_InvalidOperationException_ShouldBeReturn",
                Surname = "Test_WhenAlreadyExistAuthorNameAndSurnameIsGiven_InvalidOperationException_ShouldBeReturn",
                DateOfBirth = DateTime.Parse("1990-01-01")
              };

            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context , _mapper);
            command.Model = new CreateAuthorModel { Name = author.Name, Surname = author.Surname, DateOfBirth = author.DateOfBirth };
 
            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // arrange
            CreateAuthorCommand command = new (_context, _mapper);
            CreateAuthorModel model = new() { Name = "Tester" , Surname = "Tester" , DateOfBirth = DateTime.Parse("1990-01-01") };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke(); 

            // assert
            var author = _context.Authors.SingleOrDefault(author => author.Name == model.Name && author.Surname == model.Surname);
            author.Should().NotBeNull();                     
        } 
    }
}
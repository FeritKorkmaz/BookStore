using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord of the Rings", 0, 0, 0)]
        [InlineData("Lord of the Rings", 100, 0, 0)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 0, 1, 0)]
        [InlineData("", 0, 0, 1)]
        [InlineData("abc", 100, 1, 1)]
        [InlineData("abcd", 0 , 0, 1)]
        [InlineData("", 100, 1, 1)]     
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null)
            {
                Model = new CreateBookModel()
                {
                    Title = title,
                    PageCount = pageCount,
                    PublishDate = DateTime.Now.Date.AddYears(-1),
                    GenreId = genreId,
                    AuthorId = authorId
                }
            };

            // act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);            
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGivenEqual_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null)
            {
                Model = new CreateBookModel()
                {
                    Title = "Lord of the Rings",
                    PageCount = 100,
                    GenreId = 1,
                    AuthorId = 1,
                    PublishDate = DateTime.Now.Date
                }
            };

            // act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null)
            {
                Model = new CreateBookModel()
                {
                    Title = "Lord of the Rings",
                    PageCount = 100,
                    GenreId = 1,
                    AuthorId = 1,
                    PublishDate = DateTime.Now.Date.AddYears(-2)
                }
            };

            // act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
   
}
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Commands.UpdatedBook;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.UpdatedBook
{
    public class UpdateBooksQueryValidatorTests : IClassFixture<CommonTestFixture>
    {


        [Theory]
        [InlineData("", 0, 0)]
        [InlineData("Lord of the Rings", 0, 0)]
        [InlineData("Lord of the Rings", 1, 0)]
        [InlineData("Lord of the Rings", 0, 1)]
        [InlineData("abc", 1, 0)]
        [InlineData("", 1, 1)]
        [InlineData("abcd", 1, 0)]
        [InlineData("", 0, 1)]
        [InlineData("", 1, 1)]
        [InlineData("abcd", 0, 1)]        

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId, int authorId)
        {
            //arrange
            UpdatedBookCommand command = new UpdatedBookCommand(null)
            {
                Model = new UpdatedBookModel()
                {
                    Title = title,
                    GenreId = genreId,
                    AuthorId = authorId
                }
            };

            // act
            UpdateBooksQueryValidator validator = new UpdateBooksQueryValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdatedBookCommand command = new UpdatedBookCommand(null)
            {
                Model = new UpdatedBookModel()
                {
                    Title = "Lord of the Rings",                    
                    GenreId = 1,
                    AuthorId = 1,                    
                }
            };

            // act
            UpdateBooksQueryValidator validator = new UpdateBooksQueryValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

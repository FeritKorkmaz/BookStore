using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]    
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors( int TestId)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = TestId;
            

            // act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 1;
         

            // act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

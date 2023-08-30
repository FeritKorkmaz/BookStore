using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]    
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors( int TestId)
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = TestId;
            

            // act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 1;
         

            // act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

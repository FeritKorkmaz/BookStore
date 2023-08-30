using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {


        [Theory]    
        [InlineData("T")]      
        [InlineData("Tes")]
        [InlineData("T")]
        [InlineData("Tes")]               

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null)
            {
                Model = new UpdateGenreModel()
                {
                    Name = name,                            
                }
            };

            // act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData("Test")]
        [InlineData("")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(string name)
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null)
            {
                Model = new UpdateGenreModel()
                {
                    Name = name                    
                }
            };

            // act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
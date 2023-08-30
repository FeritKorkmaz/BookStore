using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {


        [Theory]    
        [InlineData("Te","T")]      
        [InlineData("Tes","T")]
        [InlineData("T","Te")]
        [InlineData("T","")]
        [InlineData("","T")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null)
            {
                Model = new UpdateAuthorModel()
                {
                    Name = name,
                    Surname = surname
                }
            };

            // act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGivenEqual_Validator_ShouldBeReturnError()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null)
            {
                Model = new UpdateAuthorModel()
                {
                    Name = "Test",
                    Surname = "Test",
                    DateOfBirth = DateTime.Now,
                }
            };

            // act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Tes","Te")]
        [InlineData("Test","Test")]
        [InlineData("","")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(string name, string surname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null)
            {
                Model = new UpdateAuthorModel()
                {
                    Name = name,
                    Surname = surname                                       
                }
            };

            // act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
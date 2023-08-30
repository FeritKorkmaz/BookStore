using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","")]
        [InlineData("Te","T")]
        [InlineData("Test","T")]
        [InlineData("T","Test")]                

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null)
            {
                Model = new CreateAuthorModel()
                {
                    Name = name,
                    Surname = surname,
                    DateOfBirth = DateTime.Now.Date.AddYears(-10),                   
                }
            };

            // act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);            
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGivenEqual_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null)
            {
                Model = new CreateAuthorModel()
                {
                    Name = "Test",
                    Surname = "Test",
                    DateOfBirth = DateTime.Now,
                }
            };

            // act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null)
            {
                Model = new CreateAuthorModel()
                {
                    Name = "Tester",
                    Surname = "Tester",
                    DateOfBirth = DateTime.Now.AddYears(-10),
                }
            };

            // act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
   
}
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetailQuery;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]    
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors( int TestId)
        {
            //arrange
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.BookId = TestId;       

            // act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.BookId = 1;
         

            // act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
      

}

using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]    
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors( int TestId)
        {
            //arrange
            GetGenreDetailQuery command = new GetGenreDetailQuery(null, null);
            command.GenreId = TestId;       

            // act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            GetGenreDetailQuery command = new GetGenreDetailQuery(null, null);
            command.GenreId = 1;
         

            // act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
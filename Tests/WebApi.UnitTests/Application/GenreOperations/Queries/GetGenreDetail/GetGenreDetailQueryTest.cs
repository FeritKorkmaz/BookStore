using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenExistGenreIdIsNotGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arange                              
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);            
            command.GenreId = 999;
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);

            // act & assert            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadi");            
        }

        [Fact]
        public void WhenValidInputsAreGiven_GenreDetail_ShouldBeReturn()
        {
            // arrange           
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
            command.GenreId = 2;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().NotBeNull();
        }

    }


    
}
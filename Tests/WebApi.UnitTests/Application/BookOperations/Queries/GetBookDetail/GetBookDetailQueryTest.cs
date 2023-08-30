using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetailQuery;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenExistBookIdIsNotGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arange                              
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);            
            command.BookId = 999;
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);

            // act & assert            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadi");            
        }

        [Fact]
        public void WhenValidInputsAreGiven_BookDetail_ShouldBeReturn()
        {
            // arrange           
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = 2;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().NotBeNull();
        }

    }


    
}
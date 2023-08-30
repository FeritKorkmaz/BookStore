using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenExistAuthorIdIsNotGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arange                              
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);            
            command.AuthorId = 999;
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);

            // act & assert            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadi");          
        }

        [Fact]
        public void WhenValidInputsAreGiven_AuthorDetail_ShouldBeReturn()
        {
            // arrange           
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorId = 2;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);
            author.Should().NotBeNull();
        }

    }


    
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorsDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }



        public AuthorDetailViewModel Handle()
        {
            var author = _dbContext.Authors.Where(author => author.Id == AuthorId).SingleOrDefault();
            if (author == null)
            {
                throw new InvalidOperationException("Yazar bulunamadi");
            }
            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);
            return vm;  
        }
    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
    }
}
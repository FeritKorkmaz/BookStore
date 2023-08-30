using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery 
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;       
        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);            
            if(genre == null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadi");
            }
            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
}
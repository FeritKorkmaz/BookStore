using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; } 
        private readonly IBookStoreDbContext _context;
        public UpdateGenreCommand( IBookStoreDbContext context)
        {
           _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadi");
            }

            if(_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            {
                throw new InvalidOperationException("Ayni isimli bir kitap türü zaten mevcut");
            }
            genre.Name = Model.Name.Trim() == string.Empty ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdatedBook
{
    public class UpdatedBookCommand
    {
        public UpdatedBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdatedBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadi");
            }
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
             
            _dbContext.SaveChanges();
        }   
    }
    public class UpdatedBookModel
    {
        public string Title { get; set; }            
        public int GenreId { get; set; }
        public int AuthorId { get; set; }           
    }
}
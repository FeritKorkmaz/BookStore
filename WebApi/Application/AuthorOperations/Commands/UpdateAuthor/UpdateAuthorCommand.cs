using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _dbContext;       
        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;           
        }
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author == null)
            {
                throw new InvalidOperationException("Yazar Bulunamadi");
            }
            
            if(_dbContext.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname == Model.Surname && x.Id != AuthorId))
            {
                throw new InvalidOperationException("Ayni isim soyisimli bir yazar zaten mevcut");
            }

            author.Name = Model.Name.Trim() == string.Empty ? author.Name : Model.Name;
            author.Surname = Model.Surname.Trim() == string.Empty ? author.Surname : Model.Surname;
            

            _dbContext.SaveChanges();
        }
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
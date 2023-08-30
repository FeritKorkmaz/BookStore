using WebApi.DBOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks( this BookStoreDbContext context )
        {
            context.Books.AddRange(
                new Book{ Title = "Lean Startup", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2000, 01, 01), AuthorId = 1 },
                new Book{ Title = "Herland", GenreId = 2, PageCount = 250, PublishDate = new DateTime(2010, 05, 25), AuthorId = 2 },
                new Book{ Title = "Dune", GenreId = 2, PageCount = 500, PublishDate = new DateTime(2005, 05, 05), AuthorId = 3 });           
        }
    }
    
}
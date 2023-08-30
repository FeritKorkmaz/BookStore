using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenarator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                // Look for any book.
                if (context.Books.Any())
                {
                    return; // Data was already seeded
                }
                context.Authors.AddRange(
                    new Author
                    {
                        Name = "John",
                        Surname = "Doe",
                        DateOfBirth = new DateTime(1990, 01, 01)
                    },
                    new Author
                    {
                        Name = "Jane",
                        Surname = "Doe",
                        DateOfBirth = new DateTime(1990, 01, 01)
                    },
                    new Author
                    {
                        Name = "Mary",
                        Surname = "Smith",
                        DateOfBirth = new DateTime(1990, 07, 25)
                    }
                );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personel Growth"                         
                    },
                    new Genre
                    {
                        Name = "Scince Finction"                         
                    },
                    new Genre
                    {
                        Name = "Romence"                         
                    }
                );  

                context.Books.AddRange(
                    new Book{
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2000, 01, 01),
                        AuthorId = 1
                    },
                    new Book{
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 25),
                        AuthorId = 2
                    },
                    new Book{
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2,
                        PageCount = 500,
                        PublishDate = new DateTime(2005, 05, 05),
                        AuthorId = 3
                    }
                );

                context.SaveChanges();

            }
        }
    }
}
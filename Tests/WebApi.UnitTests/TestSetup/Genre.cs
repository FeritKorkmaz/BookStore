using WebApi.DBOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres( this BookStoreDbContext context )
        {
            context.Genres.AddRange(
                new Genre{ Name = "Personel Growth" },
                new Genre{ Name = "Scince Finction" },
                new Genre{ Name = "Romence" }
            );
        }
    }
}
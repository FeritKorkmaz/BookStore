using WebApi.DBOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
        public static class Authors
        {
            public static void AddAuthors( this BookStoreDbContext context )
            {
                context.Authors.AddRange(
                    new Author { Name = "John", Surname = "Doe", DateOfBirth = new DateTime(1990, 01, 01) },
                    new Author { Name = "Jane", Surname = "Doe", DateOfBirth = new DateTime(1990, 01, 01) },
                    new Author { Name = "Mary", Surname = "Smith", DateOfBirth = new DateTime(1990, 07, 25) } 
                );
            }
        }

}


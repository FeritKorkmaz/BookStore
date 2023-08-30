using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetBookDetailQuery;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Entities;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsDetail;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();

            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));           
           
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));

            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<CreateAuthorModel, Author>();            
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();

            CreateMap<CreateUserModel, User>();  

        }

    }
}
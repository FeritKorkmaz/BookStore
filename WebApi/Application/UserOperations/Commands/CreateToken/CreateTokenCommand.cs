using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user != null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAcsessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();

                return token;
            }
            else
            {
                throw new InvalidOperationException("Kullanici adi veya sifre hatali");
            }

        }
    }

  

    public class CreateTokenModel
    {        
        public string? Email { get; set; }
        public string ?Password { get; set; }
    }
}
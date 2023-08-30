using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.DBOperations;
using WebApi.MiddleWares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Token:Issuer"],
        ValidAudience = configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my custom secret key for authentication")),
        ClockSkew = TimeSpan.Zero
    };

});


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName:"BookStoreDB"));
builder.Services.AddScoped<IBookStoreDbContext, BookStoreDbContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles();

}

app.UseAuthentication();
 
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExeptionMiddleWare();

app.UseCors("AllowOrigin");
app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.MapControllers();

//1. Find the service layer within our scope.
var scope = app.Services.CreateScope();

//2. Get the instance of BoardGamesDBContext in our services layer
var services = scope.ServiceProvider;

//3. Call the DataGenerator to create sample data
DataGenarator.Initialize(services);

//Continue to run the application
app.Run();

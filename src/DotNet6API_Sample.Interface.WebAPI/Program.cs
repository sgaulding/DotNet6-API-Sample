using DotNet6API_Sample.Library.Interfaces;
using DotNet6API_Sample.Library.Repositories;
using DotNet6API_Sample.Library.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Connection String
var dotnet6DbConnectionString = builder.Configuration.GetValue<string>("ConnectionStrings:dotnet6-api-sample-mssql");

// Setup Dependence Injection
builder.Services.AddOptions();
builder.Services.AddSingleton(new UserDbContext(dotnet6DbConnectionString));
builder.Services.AddSingleton<IUserRepository, EntityFrameworkUserRepository>();
builder.Services.AddSingleton<UserService>();

// Force url to lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
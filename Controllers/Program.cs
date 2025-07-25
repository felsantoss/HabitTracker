using Configuration.Data;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.UserRepository;
using Services.Interface;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
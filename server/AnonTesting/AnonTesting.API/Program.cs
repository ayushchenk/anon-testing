using AnonTesting.BLL.AutoMapperProfiles;
using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Services;
using AnonTesting.DAL.Interfaces;
using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();

string connectionString = appSettings.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(ApplicationProfile));
builder.Services.AddScoped<IEntityRepository<Test>, TestRepository>();
builder.Services.AddScoped<ITestService, TestService>();

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
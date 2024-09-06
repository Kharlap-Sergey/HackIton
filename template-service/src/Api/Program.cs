using Api.Domain;
using Api.Infrastructure;
using Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Configuration["DataBase:Provider"] == "InMemory")
{
    builder.Services.AddDbContext<DataDbContext>(options =>
    {
        options.UseInMemoryDatabase("ApiDb");
    });
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("Postgres db connection string is missed");
    Console.WriteLine(connectionString);
    builder.Services.AddPgStorage(connectionString);
}

builder.Services.AddEntityRepo();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.MapGet("/", (IReadOnlyRepo<Entity, int> repo) =>
{
    return repo.GetAsync();
})
.WithName("GetAll")
.WithOpenApi();

app.MapPost("/", ([FromBody]EntityCreateVm entity, IRepo<Entity, int?> repo) =>
{
    return repo.AddAsync(new Entity { Name = entity.name });
})
.WithName("Add")
.WithOpenApi();

app.Run();

internal record EntityCreateVm(string name);

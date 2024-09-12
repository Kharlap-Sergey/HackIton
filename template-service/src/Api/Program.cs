using Api.Application.HostedServices;
using Api.Configurations;
using Api.Domain;
using Api.Infrastructure;
using Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

var frontendCorsPolicyName = "FrontendCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TelegramBotConfigs>(builder.Configuration.GetSection(nameof(TelegramBotConfigs)));
builder.Services.AddHostedService<TgWebhookHostedService>();

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
builder.Services.AddScoped<ITgBotClient, TgBotClient>();

var botToken = builder.Configuration["TelegramBotConfigs:BotToken"];
builder.Services.AddHttpClient("tgwebhook")
    .RemoveAllLoggers()
    .AddTypedClient<ITelegramBotClient>(client => new TelegramBotClient(botToken, client));

var frontendUrl = builder.Configuration["FrontendUrl"];
builder.Services.AddCors(options =>
{
    options.AddPolicy(frontendCorsPolicyName, builder =>
    {
        builder.WithOrigins(frontendUrl)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(frontendCorsPolicyName);

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

app.MapPost("/bot", async (ITgBotClient tgClient, Update update) =>
{
    if (update.Message == null)
    {
        return Results.Ok();
    }

    await tgClient.SendTextMessageAsync(update.Message.Chat.Id, "Hello, world!");
    return Results.Ok();
});

app.Run();

internal record EntityCreateVm(string name);

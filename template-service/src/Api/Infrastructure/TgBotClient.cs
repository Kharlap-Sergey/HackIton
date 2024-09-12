using Api.Domain;
using Telegram.Bot;

namespace Api.Infrastructure;

public class TgBotClient(ITelegramBotClient client) : ITgBotClient
{
    public async Task SendTextMessageAsync(long id, string message, CancellationToken cancellationToken = default)
    {
        await client.SendTextMessageAsync(id, message, cancellationToken: cancellationToken);
    }
}

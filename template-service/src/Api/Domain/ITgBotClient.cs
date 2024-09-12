namespace Api.Domain;

public interface ITgBotClient
{
    Task SendTextMessageAsync(long id, string message, CancellationToken cancellationToken = default);
}

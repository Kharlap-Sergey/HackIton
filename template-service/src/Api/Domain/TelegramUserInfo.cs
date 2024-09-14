using Api.Enums;

namespace Api.Domain;

public class TelegramUserInfo : Entity<long>
{
    public int ChatId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int VerificationCode { get; set; }
    public TgAuthStatuses AuthStatus { get; set; }
    public DateTime CodeSentDate { get; set; }
}

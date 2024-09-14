using Api.Domain;
using Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure;

public class TelegramUserInfoRepository : RepositoryBase<TelegramUserInfo, long>, ITelegramUserInfoRepository
{
    public TelegramUserInfoRepository(DataDbContext dataDbContext) : base(dataDbContext)
    { }

    public async ValueTask<bool> IsEmailTakenAsync(string email)
    {
        var lowerEmail = email.ToLower().Trim();
        return await _context.TelegramUsers.AnyAsync(user => user.Email == lowerEmail);
    }

}

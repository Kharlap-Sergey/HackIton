using Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<TelegramUserInfo> TelegramUsers { get; set; }
    }
}

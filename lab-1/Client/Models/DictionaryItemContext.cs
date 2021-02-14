using Microsoft.EntityFrameworkCore;

namespace Client.Models
{
    public class DictionaryItemContext : DbContext
    {
        public DbSet<DictionaryItem> DictionaryItems { get; set; }

        public DictionaryItemContext(DbContextOptions<DictionaryItemContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
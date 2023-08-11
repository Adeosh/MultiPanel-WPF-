using Microsoft.EntityFrameworkCore;
using MultiPanel_WPF_.MVVM.Model;

namespace MultiPanel_WPF_.Core
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MusicStore.db");
        }

        public DbSet<Album> Albums { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=192.168.60.45;Port=5432;Database=stapi;Username=stapi;Password=stapi$1234");
    }
}
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
        {
        }

        //public DbSet<Blog> Blogs { get; set; }

        //public DbSet<Post> Posts { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoUser> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //=> optionsBuilder.UseNpgsql("Host=192.168.58.45;Port=5432;Database=stapi;Username=stapi;Password=test$1234");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(b => b.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<TodoUser>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TodoUser>()
                .Property(i => i.Id);

            modelBuilder.Entity<TodoUser>()
                .HasMany(i => i.Blogs)
                .WithOne(i => i.User);
        }

    }
}
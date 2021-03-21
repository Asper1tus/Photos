using Microsoft.EntityFrameworkCore;
using Photos.Date.Entities;

namespace Photos.Date.EntityFramework
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
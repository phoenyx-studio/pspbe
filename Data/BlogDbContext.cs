using Microsoft.EntityFrameworkCore;
using pspbe.Models;

namespace pspbe.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Category> Categories {get; set;}
        public virtual DbSet<Post> Posts {get; set;}
    }
}

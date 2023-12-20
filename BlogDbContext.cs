using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models.Domain;

namespace SimpleBlog.Data
{
    public class BlogDbContext:DbContext
    {
        public BlogDbContext(DbContextOptions options):base(options)
        {}
        public DbSet<BlogInfo> Blogs{ get; set; }

    }
}

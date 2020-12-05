using GraphQLWithHotChocolate.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLWithHotChocolate.Context 
{
    public class DemoContext : DbContext {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder options) => options.UseSqlite ("Data Source=blogs.db");
    }
}
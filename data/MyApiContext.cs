using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Data
{

    public class MyApiContext : DbContext
    {
        public MyApiContext(DbContextOptions<MyApiContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }

        public DbSet<Posts> Posts { get; set; }

    }
}
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<Message>().Property(p => p.Id).UseIdentityColumn(seed: 1, increment: 1);
        }
    }
}

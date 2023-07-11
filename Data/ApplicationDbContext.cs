using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Post>? Posts { get; set; }
        public DbSet<UserExtraStuff>? UserExtraStuff { get; set; }
        public DbSet<HomeScreenData>? HomeScreenData { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationship between Posts and Users
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                //Was - .OnDelete(DeleteBehavior.Restrict);
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
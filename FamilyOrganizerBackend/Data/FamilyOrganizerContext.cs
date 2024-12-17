using FamilyOrganizerBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyOrganizerBackend.Data
{
    public class FamilyOrganizerContext : DbContext
    {
        public FamilyOrganizerContext(DbContextOptions<FamilyOrganizerContext> options)
            : base(options)
        {
        }

        public DbSet<DinnerPoll> DinnerPolls { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
        public DbSet<Chore> Chores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<Contribution> Contributions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FamilyOrganizerDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();

            modelBuilder.Entity<Contribution>()
                .Property(c => c.Amount)
                .HasColumnType("decimal(18,2)"); // Specify precision and scale for Amount
        }
    }
}
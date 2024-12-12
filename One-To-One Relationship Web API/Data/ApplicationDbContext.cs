using Microsoft.EntityFrameworkCore;
using Test.Models.Enitities;

namespace Test.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
              .HasOne(e => e.EmployeeDetails)
              .WithOne(ed => ed.Employee)
              .HasForeignKey<EmployeeDetails>(ed => ed.EmployeeId);


            base.OnModelCreating(modelBuilder);
        }
    }
}

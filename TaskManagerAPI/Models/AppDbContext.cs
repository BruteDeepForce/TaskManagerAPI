using Microsoft.EntityFrameworkCore;

namespace TaskManagerAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext() { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Project> Projects { get; set; }

    }
}

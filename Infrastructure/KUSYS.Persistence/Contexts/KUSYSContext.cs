using KUSYS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KUSYS.Persistence.Contexts
{
    /// <summary>
    /// Veritabanı contexti. Entitylerimiz ve context oluşturulurken yapılacak configrationlar
    /// </summary>
    public class KUSYSContext : DbContext
    {
        public KUSYSContext()
        {
        }
        public KUSYSContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "Server=(local)\\sqlexpress;Database=KUSYS;Trusted_Connection=True;MultipleActiveResultSets=True;";
                optionsBuilder.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }
    }
}

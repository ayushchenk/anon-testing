using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AnonTesting.DAL.Model
{
    public class ApplicationContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public virtual DbSet<Test> Tests { set; get; } = null!;
        public virtual DbSet<Question> Questions { set; get; } = null!;
        public virtual DbSet<Answer> Answers { set; get; } = null!;
        public virtual DbSet<TestResult> TestResults { set; get; } = null!;

        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var appSettings = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();

                string connectionString = appSettings.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}

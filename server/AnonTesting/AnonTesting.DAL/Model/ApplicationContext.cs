using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AnonTesting.DAL.Model
{
    public class ApplicationContext: IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DbSet<Test> Tests { set; get; } = null!;
        public DbSet<Question> Questions { set; get; } = null!;
        public DbSet<Answer> Answers { set; get; } = null!;
        public DbSet<TestResult> TestResults { set; get; } = null!;

        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var appSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                string connectionString = appSettings.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}

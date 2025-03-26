using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoAPI.Models;

namespace TodoAPI.AppDataContext
{
    public class TodoDbContext : DbContext
    {
        // To store the connection string
        private readonly DbSettings? _dbSettings;

        //Constructor to inject the connection string
        public TodoDbContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings?.Value ?? throw new ArgumentNullException(nameof(dbSettings));
        }

        //DbSet proerty to represent the Todo table
        public DbSet<Todo> Todos { get; set; }

        //Configuring the database provider and connection string 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_dbSettings?.ConnectionString == null)
            {
                throw new InvalidOperationException("Connection string is not configured.");
            }
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }

        //Configurel model for Todo entity
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("TodoAPI").HasKey(t => t.Id);
        }
    }
}

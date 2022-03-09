using Microsoft.EntityFrameworkCore;

namespace TodoListAPI.Entities
{
    // Class that is responsible for communication with the database
    public class TodoDbContext : DbContext
    {
        private string _connectionString = "server=localhost;user=root;database=TodoDB;password=;port=3306";
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(32);
            modelBuilder.Entity<Todo>()
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(3200);
            modelBuilder.Entity<Todo>()
                .Property(x => x.ExpireDate)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }
    }
}

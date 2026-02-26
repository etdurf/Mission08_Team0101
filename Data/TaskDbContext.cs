using Microsoft.EntityFrameworkCore;
using Mission08_Team0101.Models;

namespace Mission08_Team0101.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options) { }

        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Home" },
                new Category { CategoryId = 2, CategoryName = "School" },
                new Category { CategoryId = 3, CategoryName = "Work" },
                new Category { CategoryId = 4, CategoryName = "Church" }
            );

            // Optional: Seed some sample tasks for testing
            modelBuilder.Entity<ToDoTask>().HasData(
                new ToDoTask
                {
                    TaskId = 1,
                    TaskName = "Finish homework",
                    DueDate = new DateTime(2025, 3, 1),
                    Quadrant = 1,
                    CategoryId = 2,
                    Completed = false
                },
                new ToDoTask
                {
                    TaskId = 2,
                    TaskName = "Plan vacation",
                    DueDate = null,
                    Quadrant = 2,
                    CategoryId = 1,
                    Completed = false
                }
            );
        }
    }
}
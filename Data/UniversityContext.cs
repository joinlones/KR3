using Microsoft.EntityFrameworkCore;
using UniversityApp.Models;

namespace UniversityApp.Data
{
    public class UniversityContext :DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                    new Student{Id = 1,Name = "Daniil",Age = 20, Score = 8.5 },
                    new Student{ Id = 2, Name = "Alexey", Age = 22,Score = 9.1 },
                    new Student{Id = 3, Name = "Michail", Age = 21, Score = 7.3 },
                    new Student{Id = 4, Name = "Sophia", Age = 20, Score = 8.7 },
                    new Student{Id = 5, Name = "Maria", Age = 23, Score = 6.9 }
            );
        }

        public DbSet<Student> Students { get; set; }
    }
}

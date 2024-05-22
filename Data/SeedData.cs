using Microsoft.EntityFrameworkCore;
using UniversityApp.Models;

namespace UniversityApp.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UniversityContext(serviceProvider.GetRequiredService<DbContextOptions<UniversityContext>>()))
            {
                // Проверка, существует ли база данных, и её создание, если необходимо
                context.Database.EnsureCreated();

                // Добавление студентов, если их еще нет
                //if (!context.Students.Any())
                //{
                //    context.Students.AddRange(
                //    new Student(1, "Daniil", 20, 8.5),
                //    new Student(2, "Alexey", 22, 9.1),
                //    new Student(3, "Michail", 21, 7.3),
                //    new Student(4, "Sophia", 20, 8.7),
                //    new Student(5, "Maria", 23, 6.9)
                //); ;
                //    context.SaveChanges();
                //}
            }
        }
    }
}

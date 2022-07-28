using KUSYS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KUSYS.Persistence.Contexts
{
    /// <summary>
    /// context oluşturulduğunda Seed(default/başlangıç) verileri oluşturan yapi
    /// Öğrenci ve Ders değerleri eklendi
    /// </summary>
    public class SeedData
    {
        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration.GetConnectionString("KUSYS"));

            var context = new KUSYSContext(dbContextBuilder.Options);

            if (!context.Courses.Any())
            {
                await context.Courses.AddRangeAsync(GetCourses());
                await context.SaveChangesAsync();
            }

            if (!context.Students.Any())
            {
                await context.Students.AddRangeAsync(GetStudents());
                await context.SaveChangesAsync();
            }
        }

        private List<Student> GetStudents()
        {
            var students = new List<Student>();
            students.Add(new Student { FirstName = "Ramazan", LastName = "Canbaba", StudentNo = "123456", BirthDate = new DateTime(1993, 05, 27) });
            students.Add(new Student { FirstName = "Kübra", LastName = "Canbaba", StudentNo = "123457", BirthDate = new DateTime(1993, 05, 27) });

            return students;
        }

        private List<Course> GetCourses()
        {
            var courses = new List<Course>();
            courses.Add(new Course { CourseCode = "CSI101", CourseName = "Introduction to Computer Science" });
            courses.Add(new Course { CourseCode = "CSI102", CourseName = "Algorithms" });
            courses.Add(new Course { CourseCode = "MAT101", CourseName = "Calculus" });
            courses.Add(new Course { CourseCode = "PHY101", CourseName = "Physics" });
            return courses;
        }
    }
}
using KUSYS.Domain.Entities;
using KUSYS.Persistence.Contexts;
using KUSYS.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KUSYS.UnitTest
{
    public class StudentTest
    {
       
        public KUSYSContext GetDbContext()
        {

            var connStr = "Server=(local)\\sqlexpress;Database=KUSYS;Trusted_Connection=True;MultipleActiveResultSets=True;";
            var options = new DbContextOptionsBuilder<KUSYSContext>()
                                  .UseSqlServer(connStr)
                                  .Options;
            return new KUSYSContext(options);
        }

        [Fact]
        public async Task should_match_first_student_number_with_give_number()
        {
            // Arrange
            string studentNo = "123456782";
            using var dbContext = GetDbContext();

            // Act
            var service = new StudentRepository(dbContext);
            var result = await service.GetByIdAsync(1);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(studentNo, result.StudentNo);
        }


        [Fact]
        public async Task should_be_selected_course_first_student()
        {
            // Arrange
            using var dbContext = GetDbContext();

            // Act
            var service = new StudentRepository(dbContext);
            var result = await service.GetSingleAsync(i=> i.Id == 1, i=> i.Courses);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(result.Courses.Count > 0);
        }
    }
}
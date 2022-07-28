using KUSYS.Application.Abstracts;
using KUSYS.Domain.Entities;
using KUSYS.Persistence.Contexts;

namespace KUSYS.Persistence.Repositories
{
    /// <summary>
    /// GenericRepositoryden türeyen ve ICourseRepository den implemente edilen repository concrete ediliyor
    /// </summary>
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(KUSYSContext dbContext) : base(dbContext)
        {
        }
    }
}

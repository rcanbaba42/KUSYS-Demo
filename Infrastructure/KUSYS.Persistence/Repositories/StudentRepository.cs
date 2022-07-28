using KUSYS.Application.Abstracts;
using KUSYS.Domain.Entities;
using KUSYS.Persistence.Contexts;

namespace KUSYS.Persistence.Repositories
{
    /// <summary>
    /// GenericRepositoryden türeyen ve IStudentRepository den implemente edilen repository concrete ediliyor
    /// </summary>
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(KUSYSContext dbContext) : base(dbContext)
        {
        }
    }
}

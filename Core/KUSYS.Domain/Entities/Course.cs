using KUSYS.Domain.Entities.Common;

namespace KUSYS.Domain.Entities
{
    public class Course : BaseEntity
    {
        public Course()
        {
            Students = new List<Student>();
        }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }

}

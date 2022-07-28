using KUSYS.Domain.Entities.Common;

namespace KUSYS.Domain.Entities
{
    public class Student : BaseEntity
    {
        public Student()
        {
            Courses = new List<Course>();   
        }
        public string StudentNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }

}

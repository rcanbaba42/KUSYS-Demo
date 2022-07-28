namespace KUSYS.Domain.Dtos
{
    /// <summary>
    /// studentin kurslari için eklenmiş data transfer object
    /// Selected flag i ile tüm courselar içinde ders seçimi yapılmış olanları işaretleyebiliyoruz
    /// </summary>
    public class StudentCourse
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public bool Selected { get; set; }
    }
}

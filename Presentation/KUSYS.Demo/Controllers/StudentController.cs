using KUSYS.Application.Abstracts;
using KUSYS.Application.Utilities.Results;
using KUSYS.Domain.Dtos;
using KUSYS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Demo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        /// <summary>
        /// projeye dahil edilen servis ve repositoryler arasından dependency injection kullanılarak
        /// kullanılacak repository enjekte ediliyor
        /// </summary>
        /// <param name="studentRepository"></param>
        /// <param name="courseRepository"></param>
        public StudentController(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var student = await _studentRepository.GetAll();
            return View(student);
        }
        public async Task<IActionResult> Update(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return View(student);
        }
        /// <summary>
        /// post metodu ile gelen student nesnemize create/update işlemi 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            try
            {
                if (!ModelState.IsValid) { return View(student); }
                if (student.Id > 0)
                {
                    var result = _studentRepository.UpdateAsync(student).Result;
                }
                else
                {
                    var result = _studentRepository.AddAsync(student).Result;
                }
            }
            catch (Exception)
            {
                return View(student);
            }

            return RedirectToAction("Index", "Student");
        }
        /// <summary>
        /// post metodu ile id si alınan student detayı gönderiliyor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Detail(long id)
        {
            try
            {
                var result = _studentRepository.GetByIdAsync(id).Result;
                return Json(new SuccessDataResult<Student>(result));
            }
            catch (Exception)
            {
                return Json(new ErrorResult("Öğrenci detayı alınamadı."));
            }
        }
        /// <summary>
        /// post metodu ile id si alınan student silme işlemi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                var result = await _studentRepository.DeleteAsync(id);
                return Json(new SuccessResult("Öğrenci silindi"));
            }
            catch (Exception)
            {
                return Json(new ErrorResult("Silme işlemi başarısız."));
            }
        }
        /// <summary>
        /// post metodu ile id si alınan student dersleri gönderiliyor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetCourses(int id)
        {
            try
            {
                var allCourses = _courseRepository.GetAll().Result;
                var student = _studentRepository.GetSingleAsync(i => i.Id == id, i=> i.Courses).Result;
                if (student is null)
                    return Json(new ErrorResult("Öğrenci bulunamadı"));
                var result = allCourses.Select(i => new StudentCourse
                {
                    CourseCode = i.CourseCode,
                    CourseName = i.CourseName,
                    Selected = student.Courses.Any(a => a.CourseCode == i.CourseCode)
                }).ToList();
                return Json(new SuccessDataResult<List<StudentCourse>>(result));
            }
            catch (Exception)
            {
                return Json(new ErrorResult("Dersler bulunamadı."));

            }
        }
        /// <summary>
        /// post metodu ile id si alınan student için gelen courseCode ile course ekleme işlemi
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> AddCourse(int studentId, string code)
        {
            try
            {
                var student = _studentRepository.GetSingleAsync(i => i.Id == studentId, i => i.Courses).Result;
                if (student is null)
                    return Json(new ErrorResult("Öğrenci bulunamadı"));
                if (student.Courses.Any(i => i.CourseCode == code))
                {
                    student.Courses.Remove(student.Courses.First(i => i.CourseCode == code));
                    await _studentRepository.UpdateAsync(student);
                    return Json(new SuccessResult("Ders silindi"));
                }
                else
                {
                    var course = await _courseRepository.GetSingleAsync(i=> i.CourseCode == code);
                    student.Courses.Add(course);
                    await _studentRepository.UpdateAsync(student);
                    return Json(new SuccessResult("Ders eklendi"));
                }
            }
            catch (Exception)
            {
                return Json(new ErrorResult("Ders ekleme/silme işlemi başarısız."));
            }
        }
    }
}
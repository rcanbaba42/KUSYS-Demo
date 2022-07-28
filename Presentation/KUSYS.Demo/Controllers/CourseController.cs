using KUSYS.Application.Abstracts;
using KUSYS.Application.Utilities.Results;
using KUSYS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Demo.Controllers
{
    public class CourseController : Controller
    {
        
        private readonly ICourseRepository _courseRepository;
        /// <summary>
        /// projeye dahil edilen servis ve repositoryler arasından dependency injection kullanılarak
        /// kullanılacak repository enjekte ediliyor
        /// </summary>
        /// <param name="courseRepository"></param>
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAll();
            return View(courses); 
        }
        public async Task<IActionResult> Update(int id)
        {
            var student = await _courseRepository.GetByIdAsync(id);
            return View(student);
        }
        /// <summary>
        /// post metodu ile gelen course nesnemize create/update işlemi 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(Course course)
        {
            try
            {
                if (!ModelState.IsValid) { return View(course); }
                if (course.Id > 0)
                {
                    var result = _courseRepository.UpdateAsync(course).Result;
                }
                else
                {
                    var result = _courseRepository.AddAsync(course).Result;
                }
            }
            catch (Exception)
            {
                return View(course);
            }

            return RedirectToAction("Index", "Course");
        }
        /// <summary>
        /// post metodu ile id si alınan course silme işlemi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                var result = await _courseRepository.DeleteAsync(id);
                return Json(new SuccessResult("Ders silindi"));
            }
            catch (Exception)
            {
                return Json(new ErrorResult("Silme işlemi başarısız."));
            }
        }

    }
}

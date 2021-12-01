using DatabasesProject1.Models;
using DatabasesProject1.Repositories.Interfaces;
using DatabasesProject1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DatabasesProject1.Controllers
{
    public class CoursesMaterialsController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IMaterialsRepository _materialsRepository;

        public CoursesMaterialsController(ICoursesRepository coursesRepository, IMaterialsRepository materialsRepository)
        {
            _coursesRepository = coursesRepository;
            _materialsRepository = materialsRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Courses"] = new SelectList(_coursesRepository.Read(), "CourseId", "CourseName");
            ViewData["Materials"] = new SelectList(_materialsRepository.Read(), "MaterialId", "MaterialName");

            return View();
        }

        [HttpPost]
        public IActionResult Index(CoursesMaterialsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var material = _materialsRepository.Find(model.MaterialId);
                var course = _coursesRepository.Find(model.CourseId);
                if(course.Materials == null)
                {
                    course.Materials = new List<MaterialBase>();
                }
                course.Materials.Add(material);
                _coursesRepository.Update(course);
                return RedirectToAction(nameof(Success));
            }
            return View(nameof(Index));
        }

        public IActionResult Success()
        {
            return View();
        }


        
    }
}

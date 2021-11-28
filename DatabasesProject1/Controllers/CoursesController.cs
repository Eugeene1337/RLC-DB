using DatabasesProject1.Models;
using DatabasesProject1.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DatabasesProject1.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ILanguagesRepository _languagesRepository;

        public CoursesController(ICoursesRepository coursesRepository, ILanguagesRepository languagesRepository)
        {
            _coursesRepository = coursesRepository;
            _languagesRepository = languagesRepository;
        }

        [HttpGet]
        public ActionResult<IList<Course>> Index() => View(_coursesRepository.Read());

        [HttpGet]
        public ActionResult<Course> Details(string id)
        {
            return View(_coursesRepository.Find(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var languages = _languagesRepository.Read();
            ViewData["Languages"] = new SelectList(_languagesRepository.Read(), "LanguageId", "LanguageName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Language> Create(Course item)
        {
            var language = _languagesRepository.Find(item.Language.LanguageId);
            item.Language.LanguageName = language.LanguageName;

            _coursesRepository.Create(item);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Course> Edit(string id)
        {
            var languages = _languagesRepository.Read();
            ViewData["Languages"] = new SelectList(_languagesRepository.Read(), "LanguageId", "LanguageName");

            return View(_coursesRepository.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course item)
        {
            var language = _languagesRepository.Find(item.Language.LanguageId);
            item.Language.LanguageName = language.LanguageName;


            _coursesRepository.Update(item);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _coursesRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _coursesRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

using DatabasesProject1.Models;
using DatabasesProject1.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DatabasesProject1.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly ILanguagesRepository _languagesRepository;

        public LanguagesController(ILanguagesRepository languagesRepository)
        {
            _languagesRepository = languagesRepository;
        }

        [HttpGet]
        public ActionResult<IList<Language>> Index() => View(_languagesRepository.Read());

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Language> Create(Language item)
        {
            if (ModelState.IsValid)
            {
                _languagesRepository.Create(item);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Language> Edit(string id) =>
            View(_languagesRepository.Find(id));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Language item)
        {
            if (ModelState.IsValid)
            {
                _languagesRepository.Update(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _languagesRepository.Find(id);
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
            _languagesRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

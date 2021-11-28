using DatabasesProject1.Models;
using DatabasesProject1.Repositories.Interfaces;
using DatabasesProject1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DatabasesProject1.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly IMaterialsRepository _materialsRepository;

        public MaterialsController(IMaterialsRepository materialsRepository)
        {
            _materialsRepository = materialsRepository;
        }

        [HttpGet]
        public ActionResult<IList<MaterialViewModel>> Index()
        {
            var list = new List<MaterialViewModel>();
            var texts = _materialsRepository.ReadTextMaterials();
            var videos = _materialsRepository.ReadVideoMaterials();
            var files = _materialsRepository.ReadFileMaterials();

            foreach(var item in texts)
            {
                var model = new MaterialViewModel()
                {
                    MaterialId = item.MaterialId,
                    MaterialName = item.MaterialName,
                    Content = item.Content,
                    ContentType = ContentType.Text,
                };
                list.Add(model);
            }
            foreach (var item in videos)
            {
                var model = new MaterialViewModel()
                {
                    MaterialId = item.MaterialId,
                    MaterialName = item.MaterialName,
                    Content = item.VideoUrl,
                    ContentType = ContentType.Video,
                };
                list.Add(model);
            }
            foreach (var item in files)
            {
                var model = new MaterialViewModel()
                {
                    MaterialId = item.MaterialId,
                    MaterialName = item.MaterialName,
                    Content = item.FileUrl,
                    ContentType = ContentType.File,
                };
                list.Add(model);
            }

            return View(list);
        }
            

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<MaterialBase> Create(MaterialViewModel item)
        {
            MaterialBase material;
            switch (item.ContentType)
            {
                case ContentType.Text:
                    material = new TextMaterial()
                    {
                        MaterialName = item.MaterialName,
                        Content = item.Content,
                    };
                    break;
                case ContentType.File:
                    material = new FileMaterial()
                    {
                        MaterialName = item.MaterialName,
                        FileUrl = item.Content,
                    };
                    break;
                case ContentType.Video:
                    material = new VideoMaterial()
                    {
                        MaterialName = item.MaterialName,
                        VideoUrl = item.Content,
                    };
                    break;
                default:
                    return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _materialsRepository.Create(material);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<MaterialBase> Edit(string id) =>
            View(_materialsRepository.Find(id));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MaterialBase item)
        {
            if (ModelState.IsValid)
            {
                _materialsRepository.Update(item);
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
            var item = _materialsRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            var model = new MaterialViewModel()
            {
                MaterialId = item.MaterialId,
                MaterialName = item.MaterialName,
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _materialsRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

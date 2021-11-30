using DatabasesProject1.Models;
using DatabasesProject1.Repositories.Interfaces;
using DatabasesProject1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet]
        public ActionResult<Course> Details(string id)
        {
            var item = _materialsRepository.Find(id);
            MaterialViewModel model;
            switch (item)
            {
                case TextMaterial m:
                    model = new MaterialViewModel()
                    {
                        MaterialId = item.MaterialId,
                        MaterialName = item.MaterialName,
                        Content = m.Content,
                        ContentType = ContentType.Text,
                    };
                    break;
                case FileMaterial m:
                    model = new MaterialViewModel()
                    {
                        MaterialId = item.MaterialId,
                        MaterialName = item.MaterialName,
                        Content = m.FileUrl,
                        ContentType = ContentType.Text,
                    };
                    break;
                case VideoMaterial m:
                    model = new MaterialViewModel()
                    {
                        MaterialId = item.MaterialId,
                        MaterialName = item.MaterialName,
                        Content = m.VideoUrl,
                        ContentType = ContentType.Text,
                    };
                    break;
                default:
                    return BadRequest();
            }

            return View(model);
        }

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
        public ActionResult<MaterialBase> Edit(string id)
        {
            var item = _materialsRepository.Find(id);
            MaterialViewModel model;
            switch (item)
            {
                case TextMaterial m:
                    model = new MaterialViewModel()
                    {
                        MaterialId = item.MaterialId,
                        MaterialName = item.MaterialName,
                        Content = m.Content,
                        ContentType = ContentType.Text,
                    };
                    break;
                case FileMaterial m:
                    model = new MaterialViewModel()
                    {
                        MaterialId = item.MaterialId,
                        MaterialName = item.MaterialName,
                        Content = m.FileUrl,
                        ContentType = ContentType.Text,
                    };
                    break;
                case VideoMaterial m:
                    model = new MaterialViewModel()
                    {
                        MaterialId = item.MaterialId,
                        MaterialName = item.MaterialName,
                        Content = m.VideoUrl,
                        ContentType = ContentType.Text,
                    };
                    break;
                default:
                    return BadRequest();
            }

            return View(model);
        }
            


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MaterialViewModel item)
        {
            MaterialBase material;
            switch (item.ContentType)
            {
                case ContentType.Text:
                    material = new TextMaterial()
                    {
                        MaterialId = item.MaterialId,
                        MaterialName = item.MaterialName,
                        Content = item.Content,
                    };
                    break;
                case ContentType.File:
                    material = new FileMaterial()
                    {
                        MaterialId = item.MaterialId,
                        MaterialName = item.MaterialName,
                        FileUrl = item.Content,
                    };
                    break;
                case ContentType.Video:
                    material = new VideoMaterial()
                    {
                        MaterialId = item.MaterialId,
                        MaterialName = item.MaterialName,
                        VideoUrl = item.Content,
                    };
                    break;
                default:
                    return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _materialsRepository.Update(material);
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

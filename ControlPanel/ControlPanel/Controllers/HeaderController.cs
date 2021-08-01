using ControlPanel.Models;
using ControlPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlPanel.Controllers
{
    [Authorize]
    [HandleError]
    public class HeaderController : Controller
    {
        HeaderRepository headerRepository = new HeaderRepository();

        // GET: Header/Create
        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(headerRepository.GetAllCategory(), "Id", "Name");

            return View();
        }

        // POST: Header/Create
        [HttpPost]
        public ActionResult Create(HeaderViewModel headerData)
        {
            ViewBag.CategoryList = new SelectList(headerRepository.GetAllCategory(), "Id", "Name");

            string headerType = headerRepository.GetCategoryType(headerData.Type);
            if (ModelState.IsValid)
            {
                if (headerData.ImageFile != null && headerData.ImageFile.ContentLength > 0)
                {
                    headerData.Image = DateTime.Now.Ticks + "_" + headerData.ImageFile.FileName;
                    string path = Path.Combine(Server.MapPath("~/All_Images"),
                                               Path.GetFileName(headerData.Image));
                    headerData.ImageFile.SaveAs(path);
                }
                if (headerRepository.Add(headerData))
                {
                    TempData["Message"] = $"{headerType.Remove(headerType.Length - 1)} created successfully";

                    return RedirectToAction("Index", new { type = headerData.Type });
                }
                TempData["ErrorMessage"] = $"{headerType.Remove(headerType.Length - 1)} could not be created";
            }
            return View(headerData);
        }

        public ActionResult Index(int type)
        {
            List<HeaderViewModel> headerDataList = headerRepository.GetByType(type);

            string headerType = headerRepository.GetCategoryType(type);

            ViewBag.Title = headerType;
            ViewBag.Screen = headerType;

            return View(headerDataList);
        }

        public ActionResult Details(long id)
        {
            HeaderViewModel header = headerRepository.Get(id);

            MainContentRepository mainContentRepository = new MainContentRepository();
            ViewBag.MainContentList = mainContentRepository.GetAllMainConentByHeaderId(id);


            string headerType = headerRepository.GetCategoryType(header.Type);
            ViewBag.Title = headerType;
            ViewBag.Screen = headerType + " details";

            return View(header);
        }

        // GET: Header/Edit/5
        public ActionResult Edit(long id)
        {
            HeaderViewModel header = headerRepository.Get(id);

            string headerType = headerRepository.GetCategoryType(header.Type);
            ViewBag.Title = headerType;
            ViewBag.Screen = "Edit " + headerType;

            return View(header);
        }

        // POST: Header/Edit/5
        [HttpPost]
        public ActionResult Edit(HeaderViewModel headerData)
        {
            HeaderViewModel header = headerRepository.Get(headerData.Id);

            string headerType = headerRepository.GetCategoryType(header.Type);
            ViewBag.Title = headerType;
            ViewBag.Screen = "Edit " + headerType;

            //Since type can be specified only on creation of header
            ModelState.Remove("Type");

            if (ModelState.IsValid)
            {
                if (headerData.ImageFile != null && headerData.ImageFile.ContentLength > 0)
                {
                    headerData.Image = DateTime.Now.Ticks + "_" + headerData.ImageFile.FileName;
                    string path = Path.Combine(Server.MapPath("~/All_Images"),
                                               Path.GetFileName(headerData.Image));
                    headerData.ImageFile.SaveAs(path);
                    ViewBag.FileName = headerData.Image;
                }
                if (headerRepository.Edit(headerData))
                {
                    TempData["Message"] = $"{headerType.Remove(headerType.Length - 1)} modified successfully";

                    return RedirectToAction("Index", new { type = header.Type });
                }
                TempData["ErrorMessage"] = $"{headerType.Remove(headerType.Length - 1)} could not be modified";
            }
            return View(headerData);
        }

        public ActionResult Delete(long id)
        {
            HeaderViewModel header = headerRepository.Get(id);

            string headerType = headerRepository.GetCategoryType(header.Type);

            if (headerRepository.Delete(id))
            {
                TempData["Message"] = $@"{header.Header} {headerType.Remove(headerType.Length - 1)} deleted Successfully";
            }
            else
            {
                TempData["Message"] = $"{header.Header} {headerType.Remove(headerType.Length - 1)} could not be Deleted";
            }
            return RedirectToAction("Index", new { type = header.Type });
        }
    }
}

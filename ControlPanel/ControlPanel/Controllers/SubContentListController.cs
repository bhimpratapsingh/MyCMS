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
    public class SubContentListController : Controller
    {
        SubContentListRepository subContentListRepository = new SubContentListRepository();

        // GET: SubContent/Details/5
        public ActionResult Details(long id)
        {
            SubContentListViewModel subContentListData = subContentListRepository.Get(id);

            ViewBag.Title = "SubContent List";
            ViewBag.Screen = "SubContent List details";

            return View(subContentListData);
        }

        // GET: SubContent/Create
        public ActionResult Create(long id)
        {
            SubContentListViewModel subContentListData = new SubContentListViewModel() { SubContentId=id };

            return View(subContentListData);
        }

        // POST: SubContent/Create
        [HttpPost]
        public ActionResult Create(SubContentListViewModel subContentListData)
        {
            if (ModelState.IsValid)
            {
                if (subContentListData.File != null && subContentListData.File.ContentLength > 0)
                {
                    subContentListData.FileName = DateTime.Now.Ticks + "_" + subContentListData.File.FileName;
                    string path = Path.Combine(Server.MapPath("~/PDF_Files"),
                                               Path.GetFileName(subContentListData.FileName));
                    subContentListData.File.SaveAs(path);
                }
                if (subContentListRepository.Add(subContentListData))
                {
                    TempData["Message"] = $"SubContent List created successfully";

                    return RedirectToAction("Details", "SubContent", new { id = subContentListData.SubContentId });
                }
                TempData["ErrorMessage"] = $"SubContent List could not be created";
            }

            return View(subContentListData);
        }

        // GET: SubContent/Edit/5
        public ActionResult Edit(long id)
        {
            SubContentListViewModel subContentListData = subContentListRepository.Get(id);

            ViewBag.Title = "SubContent List";
            ViewBag.Screen = "Edit SubContent List";

            return View(subContentListData);
        }

        // POST: SubContent/Edit/5
        [HttpPost]
        public ActionResult Edit(SubContentListViewModel subContentListData)
        {
            ViewBag.Title = "SubContent List";
            ViewBag.Screen = "Edit SubContent List";

            if (ModelState.IsValid)
            {
                if (subContentListData.File != null && subContentListData.File.ContentLength > 0)
                {
                    subContentListData.FileName = DateTime.Now.Ticks + "_" + subContentListData.File.FileName;
                    string path = Path.Combine(Server.MapPath("~/PDF_Files"),
                                               Path.GetFileName(subContentListData.FileName));
                    subContentListData.File.SaveAs(path);
                }
                if (subContentListRepository.Edit(subContentListData))
                {
                    TempData["Message"] = $"SubContent list modified successfully";

                    return RedirectToAction("Details", "SubContent", new { id = subContentListData.SubContentId });
                }
                TempData["ErrorMessage"] = $"SubContent list could not be modified";
            }

            return View(subContentListData);
        }


        public ActionResult Delete(long id)
        {
            SubContentListViewModel subContentListData = subContentListRepository.Get(id);

            if (subContentListRepository.Delete(id))
            {
                TempData["Message"] = $@"SubContent List deleted Successfully";
            }
            else
            {
                TempData["Message"] = $"SubContent List could not be Deleted";
            }
            return RedirectToAction("Details", "SubContent", new { id = subContentListData.SubContentId });
        }
    }
}
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
    public class SubContentController : Controller
    {
        SubContentRepository subContentRepository = new SubContentRepository();

        // GET: SubContent/Details/5
        public ActionResult Details(long id)
        {
            SubContentViewModel subContentData = subContentRepository.Get(id);

            SubContentListRepository subContentListRepository = new SubContentListRepository();
            ViewBag.SubContentDataList = subContentListRepository.GetAllSubContentListBySubContentId(id);


            ViewBag.Title = "SubContent";
            ViewBag.Screen = "SubContent details";

            return View(subContentData);
        }

        // GET: SubContent/Create
        public ActionResult Create(long id)
        {
            SubContentViewModel subContentData = new SubContentViewModel() { MainContentId = id };

            return View(subContentData);
        }

        // POST: SubContent/Create
        [HttpPost]
        public ActionResult Create(SubContentViewModel subContentData)
        {
            if (ModelState.IsValid)
            {
                if (subContentData.AddPage)
                {
                    if (subContentData.ImageFile != null && subContentData.ImageFile.ContentLength > 0)
                    {
                        subContentData.Image = DateTime.Now.Ticks + "_" + subContentData.ImageFile.FileName;
                        string path = Path.Combine(Server.MapPath("~/All_Images"),
                                                   Path.GetFileName(subContentData.Image));
                        subContentData.ImageFile.SaveAs(path);
                    }
                }
                else
                {
                    if (subContentData.File != null && subContentData.File.ContentLength > 0)
                    {
                        subContentData.FileName = DateTime.Now.Ticks + "_" + subContentData.File.FileName;
                        string path = Path.Combine(Server.MapPath("~/PDF_Files"),
                                                   Path.GetFileName(subContentData.FileName));
                        subContentData.File.SaveAs(path);
                    }

                    //Set page related property to null
                    subContentData.PageTitle = null;
                }

                if (subContentRepository.Add(subContentData))
                {
                    TempData["Message"] = $"SubContent created successfully";

                    return RedirectToAction("Details", "MainContent", new { id = subContentData.MainContentId });
                }
                TempData["ErrorMessage"] = $"SubContent could not be created";
            }

            return View(subContentData);
        }

        // GET: SubContent/Edit/5
        public ActionResult Edit(long id)
        {
            SubContentViewModel subContentData = subContentRepository.Get(id);

            ViewBag.Title = "SubContent";
            ViewBag.Screen = "Edit SubContent";

            return View(subContentData);
        }

        // POST: SubContent/Edit/5
        [HttpPost]
        public ActionResult Edit(SubContentViewModel subContentData)
        {
            ViewBag.Title = "SubContent";
            ViewBag.Screen = "Edit SubContent";

            if (ModelState.IsValid)
            {
                if (subContentData.AddPage)
                {
                    if (subContentData.ImageFile != null && subContentData.ImageFile.ContentLength > 0)
                    {
                        subContentData.Image = DateTime.Now.Ticks + "_" + subContentData.ImageFile.FileName;
                        string path = Path.Combine(Server.MapPath("~/All_Images"),
                                                   Path.GetFileName(subContentData.Image));
                        subContentData.ImageFile.SaveAs(path);
                        ViewBag.FileName = subContentData.Image;
                    }
                    subContentData.FileName = null;
                }
                else
                {
                    if (subContentData.File != null && subContentData.File.ContentLength > 0)
                    {
                        subContentData.FileName = DateTime.Now.Ticks + "_" + subContentData.File.FileName;
                        string path = Path.Combine(Server.MapPath("~/PDF_Files"),
                                                   Path.GetFileName(subContentData.FileName));
                        subContentData.File.SaveAs(path);
                    }

                    //Set page related property to null
                    subContentData.PageTitle = null;
                    subContentData.Image = null;

                    //delete all other pages if add page is false and file upload is true
                    //Content saving will be allowed 
                    if (string.IsNullOrEmpty(subContentData.FileName))
                    {
                        subContentRepository.DeleteSubContentListData(subContentData.Id);
                    }
                }

                if (subContentRepository.Edit(subContentData))
                {
                    TempData["Message"] = $"SubContent modified successfully";

                    return RedirectToAction("Details", "MainContent", new { id = subContentData.MainContentId });
                }
                TempData["ErrorMessage"] = $"SubContent could not be modified";
            }

            return View(subContentData);
        }


        public ActionResult Delete(long id)
        {
            SubContentViewModel subContentData = subContentRepository.Get(id);

            subContentRepository.DeleteSubContentListData(subContentData.Id);

            if (subContentRepository.Delete(id))
            {
                TempData["Message"] = $@"SubContent deleted Successfully";
            }
            else
            {
                TempData["Message"] = $"SubContent could not be Deleted";
            }
            return RedirectToAction("Details", "MainContent", new { id = subContentData.MainContentId });
        }
    }
}
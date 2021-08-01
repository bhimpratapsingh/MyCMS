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
    public class MainContentController : Controller
    {
        MainContentRepository mainContentRepository = new MainContentRepository();

        // GET: MainContent/Details/5
        public ActionResult Details(long id)
        {
            MainContentViewModel mainContentData = mainContentRepository.Get(id);

            SubContentRepository subContentRepository = new SubContentRepository();
            ViewBag.SubContentList = subContentRepository.GetAllSubContentByMainContentId(id);


            ViewBag.Title = "Content";
            ViewBag.Screen = "Content details";

            return View(mainContentData);
        }

        // GET: MainContent/Create
        public ActionResult Create(long headerId)
        {
            MainContentViewModel mainContentData = new MainContentViewModel() { HeaderId = headerId };

            return View(mainContentData);
        }

        // POST: MainContent/Create
        [HttpPost]
        public ActionResult Create(MainContentViewModel mainContentData)
        {
            if (ModelState.IsValid)
            {
                if (mainContentData.AddPage)
                {
                    if (mainContentData.ImageFile != null && mainContentData.ImageFile.ContentLength > 0)
                    {
                        mainContentData.Image = DateTime.Now.Ticks + "_" + mainContentData.ImageFile.FileName;
                        string path = Path.Combine(Server.MapPath("~/All_Images"),
                                                   Path.GetFileName(mainContentData.Image));
                        mainContentData.ImageFile.SaveAs(path);
                    }
                }
                else
                {
                    if (mainContentData.File != null && mainContentData.File.ContentLength > 0)
                    {
                        mainContentData.FileName = DateTime.Now.Ticks + "_" + mainContentData.File.FileName;
                        string path = Path.Combine(Server.MapPath("~/PDF_Files"),
                                                   Path.GetFileName(mainContentData.FileName));
                        mainContentData.File.SaveAs(path);
                    }

                    //Set all page related items to null
                    mainContentData.PageTitle = null;
                }


                if (mainContentRepository.Add(mainContentData))
                {
                    TempData["Message"] = $"Content created successfully";

                    return RedirectToAction("Details", "Header", new { id = mainContentData.HeaderId });
                }
                TempData["ErrorMessage"] = $"Content could not be created";
            }

            return View(mainContentData);
        }

        // GET: MainContent/Edit/5
        public ActionResult Edit(long id)
        {
            MainContentViewModel mainContentData = mainContentRepository.Get(id);

            ViewBag.Title = "Content";
            ViewBag.Screen = "Edit Content";

            return View(mainContentData);
        }

        // POST: MainContent/Edit/5
        [HttpPost]
        public ActionResult Edit(MainContentViewModel mainContentData)
        {
            ViewBag.Title = "Content";
            ViewBag.Screen = "Edit Content";

            if (ModelState.IsValid)
            {
                if (mainContentData.AddPage)
                {
                    if (mainContentData.ImageFile != null && mainContentData.ImageFile.ContentLength > 0)
                    {
                        mainContentData.Image = DateTime.Now.Ticks + "_" + mainContentData.ImageFile.FileName;
                        string path = Path.Combine(Server.MapPath("~/All_Images"),
                                                   Path.GetFileName(mainContentData.Image));
                        mainContentData.ImageFile.SaveAs(path);
                        ViewBag.FileName = mainContentData.Image;
                    }
                    mainContentData.FileName = null;
                }
                else
                {
                    if (mainContentData.File != null && mainContentData.File.ContentLength > 0)
                    {
                        mainContentData.FileName = DateTime.Now.Ticks + "_" + mainContentData.File.FileName;
                        string path = Path.Combine(Server.MapPath("~/PDF_Files"),
                                                   Path.GetFileName(mainContentData.FileName));
                        mainContentData.File.SaveAs(path);
                    }

                    //Set all page related items to null
                    mainContentData.PageTitle = null;
                    mainContentData.Image = null;

                    //delete all other pages if add page is false and file upload is true
                    //Content saving will be allowed 
                    if (string.IsNullOrEmpty(mainContentData.FileName))
                    {
                        mainContentRepository.DeleteSubContentAndSubContentListData(mainContentData.Id);
                    }
                }

                if (mainContentRepository.Edit(mainContentData))
                {
                    TempData["Message"] = $"Content modified successfully";

                    return RedirectToAction("Details", "Header", new { id = mainContentData.HeaderId });
                }
                TempData["ErrorMessage"] = $"Content could not be modified";
            }

            return View(mainContentData);
        }


        public ActionResult Delete(long id)
        {
            MainContentViewModel mainContentData = mainContentRepository.Get(id);

            mainContentRepository.DeleteSubContentAndSubContentListData(mainContentData.Id);

            if (mainContentRepository.Delete(id))
            {
                TempData["Message"] = $@"Content deleted Successfully";
            }
            else
            {
                TempData["Message"] = $"Content could not be Deleted";
            }
            return RedirectToAction("Details", "Header", new { id = mainContentData.HeaderId });
        }
    }
}

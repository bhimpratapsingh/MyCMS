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
    public class ClientLogoController : Controller
    {
        ClientLogoRepository repository = new ClientLogoRepository();

        // GET: ClientLogo
        public ActionResult Index()
        {
            List <ClientLogoViewModel> clientLogos = repository.GetAll();
            return View(clientLogos);
        }

        // GET: ClientLogo/Details/5
        public ActionResult Details(int id)
        {
            ClientLogoViewModel clientLogo = repository.Get(id);
            return View(clientLogo);
        }

        // GET: ClientLogo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientLogo/Create
        [HttpPost]
        public ActionResult Create(ClientLogoViewModel clientLogo)
        {
            if (ModelState.IsValid)
            {
                if (clientLogo.ImageFile != null && clientLogo.ImageFile.ContentLength > 0)
                {
                    clientLogo.Image = DateTime.Now.Ticks + "_" + clientLogo.ImageFile.FileName;
                    string path = Path.Combine(Server.MapPath("~/All_Images/ClientLogos"),
                                               Path.GetFileName(clientLogo.Image));
                    clientLogo.ImageFile.SaveAs(path);
                }
                if (repository.Add(clientLogo))
                {
                    TempData["Message"] = $"{clientLogo.Name} created successfully";

                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = $"{clientLogo.Name} could not be created";
            }
            return View(clientLogo);
        }

        // GET: ClientLogo/Edit/5
        public ActionResult Edit(int id)
        {
            ClientLogoViewModel clientLogo = repository.Get(id);

            return View(clientLogo);
        }

        // POST: ClientLogo/Edit/5
        [HttpPost]
        public ActionResult Edit(ClientLogoViewModel clientLogo)
        {
            ModelState.Remove("ImageFile");

            if (ModelState.IsValid)
            {
                if (clientLogo.ImageFile != null && clientLogo.ImageFile.ContentLength > 0)
                {
                    clientLogo.Image = DateTime.Now.Ticks + "_" + clientLogo.ImageFile.FileName;
                    string path = Path.Combine(Server.MapPath("~/All_Images/ClientLogos"),
                                               Path.GetFileName(clientLogo.Image));
                    clientLogo.ImageFile.SaveAs(path);
                    ViewBag.FileName = clientLogo.Image;
                }
                if (repository.Edit(clientLogo))
                {
                    TempData["Message"] = $"{clientLogo.Name} modified successfully";

                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = $"{clientLogo.Name} could not be modified";
            }

            ClientLogoViewModel client = repository.Get(clientLogo.Id);
            return View(client);
        }

        public ActionResult Delete(int id)
        {
            ClientLogoViewModel clientLogo= repository.Get(id);

            if (repository.Delete(id))
            {
                TempData["Message"] = $@"{clientLogo.Name} deleted Successfully";
            }
            else
            {
                TempData["Message"] = $"{clientLogo.Name} could not be Deleted";
            }
            return RedirectToAction("Index");
        }
    }
}

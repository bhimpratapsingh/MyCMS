using ControlPanel.Models;
using ControlPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlPanel.Controllers
{
    [HandleError]
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomeScreen()
        {
            HeaderRepository headerRepository = new HeaderRepository();
            List<HeaderViewModel> headerList = headerRepository.GetAllHeaders().Where(x => x.Type == (int)Category.Services || x.Type == (int)Category.Products).ToList();

            return View(headerList);
        }

        [HttpPost]
        public ActionResult HomeScreen(List<HeaderViewModel> headers)
        {
            HeaderRepository headerRepository = new HeaderRepository();

            if (ModelState.IsValid)
            {
                if (headers != null && headers.Count > 0)
                {
                    bool result = headerRepository.SaveBackColor(headers);

                    if (result)
                    {
                        TempData["Message"] = $"Back Color updated successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Back Color updated failed";
                    }
                }
                else
                {
                    headers = headerRepository.GetAllHeaders().Where(x => x.Type == (int)Category.Services || x.Type == (int)Category.Products).ToList();
                }
            }

            return View(headers);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                UserViewModel user = CommonRepository.GetSessionUser();
                UsersRepository usersRepository = new UsersRepository();

                if (!usersRepository.VerifyOldPassword(user.Id, changePasswordViewModel.OldPassword))
                {
                    TempData["ErrorMessage"] = "Old password incorrect";
                }
                else if (!usersRepository.ChangePassword(user.Id, changePasswordViewModel.Password))
                {
                    TempData["ErrorMessage"] = "Password update failed";
                }
                else
                {
                    TempData["Message"] = "Password updated successfully.";
                }
            }

            return View(changePasswordViewModel);
        }
    }
}
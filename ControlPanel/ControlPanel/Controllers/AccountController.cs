using ControlPanel.Models;
using ControlPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ControlPanel.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel account, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                AuthenticateRepository rep = new AuthenticateRepository();
                var res = rep.Authenticate(account.Username, account.Password);
                if (res == 0)
                {
                    FormsAuthentication.SetAuthCookie(account.Username, false);
                    UserViewModel sessionUser = rep.GetUser(account.Username);
                    if (sessionUser != null)
                    {
                        sessionUser.IPAddress = Request.UserHostAddress;
                        HttpContext.Session.Add("SessionUser", sessionUser);
                    }
                    if ((!String.IsNullOrEmpty(ReturnUrl)) && Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                    && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                    {
                        return Redirect(ReturnUrl ?? "~/");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else if (res == 1)
                {
                    ModelState.AddModelError("Username", "Please enter valid credentials.");
                }
                else if (res == 2)
                {
                    ModelState.AddModelError("Username", "User is in-active.");
                }
            }
            return View(account);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            // clear authentication cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
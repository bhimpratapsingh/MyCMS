using ServicesWebsite.Models;
using ServicesWebsite.ViewModels;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace ServicesWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BaseRepository baseRepository = new BaseRepository();

            //To fetch Services
            HomeScreenViewModel homeScreen = new HomeScreenViewModel();
            homeScreen.Services=  baseRepository.GetHeaderByType(1);

            //To fetch Products
            homeScreen.Products= baseRepository.GetHeaderByType(2);

            //To fetch client logos
            homeScreen.ClientLogos = baseRepository.GetAllClientLogos();

            return View(homeScreen);
        }

        public ActionResult HowWeWork()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(QueryFormViewModel queryForm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MailMessage mail = new MailMessage(Constants.QUERY_FORM_FROM_EMAIL, Constants.QUERY_FORM_TO_EMAIL))
                    {
                        mail.Subject = "Application submitted for query";
                        mail.Body = $"FirstName: {queryForm.FirstName} \n LastName: {queryForm.LastName} \n Email: {queryForm.Email} \n Phone: {queryForm.MobileNumber} \n" +
                            $" \n Message: {queryForm.Description}";

                        mail.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient
                        {
                            Host = Constants.QUERY_FORM_FROM_EMAIL_HOST_NAME,
                            EnableSsl = Constants.QUERY_FORM_FROM_EMAIL_ENABLE_SSL
                        };
                        NetworkCredential networkCredential = new NetworkCredential(Constants.QUERY_FORM_FROM_EMAIL, Constants.QUERY_FORM_FROM_EMAIL_PASSWORD);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCredential;
                        smtp.Port = Constants.QUERY_FORM_FROM_EMAIL_PORT;

                        smtp.Send(mail);
                    }

                    return Json(new { Success = true, Message = "Details Submitted Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var item in modelState.Errors)
                        {
                            return Json(new { Success = false, Message = item.ErrorMessage });
                        }
                    }
                }
            }
            catch (System.Exception ex)
            { }

            return Json(new { Success = false, Message = "Oops! Something went wrong" });
        }
    }
}
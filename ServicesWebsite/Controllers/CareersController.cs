using ServicesWebsite.ViewModels;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace ServicesWebsite.Controllers
{
    public class CareersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CareersViewModel careers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MailMessage mail = new MailMessage(Constants.QUERY_FORM_FROM_EMAIL, Constants.QUERY_FORM_TO_EMAIL))
                    {
                        mail.Subject = "Application submitted for career";
                        mail.Body = $"FirstName: {careers.FirstName} \n LastName: {careers.LastName} \n Email: {careers.Email} \n Phone: {careers.MobileNumber} \n" +
                            $"City: {careers.City} \n State: {careers.State} \n Specialization:{careers.Specialization} \n Message: {careers.Message}";

                        if (careers.Resume != null)
                        {
                            string fileName = Path.GetFileName(careers.Resume.FileName);
                            mail.Attachments.Add(new Attachment(careers.Resume.InputStream, fileName));
                        }

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
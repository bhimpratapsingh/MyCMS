using System.Web.Mvc;

namespace ServicesWebsite.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult Index(string FileName)
        {
            string file = Server.MapPath(Constants.PDF_FOLDER_URL + FileName);

            if (System.IO.File.Exists(file))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(file);

                return new FileContentResult(bytes, "application/pdf");
            }
            else
            {
                return Content(@"<div style='left:50%; top: 50%; margin: -25px 0 0 -200px; height:50px; width: 800px; position:absolute;'><h2 style='font-family:Arial;' class='login'>Sorry!<br/>File not Found.</h2></div>");
            }
        }
    }
}
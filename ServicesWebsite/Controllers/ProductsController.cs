using ServicesWebsite.Models;
using ServicesWebsite.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ServicesWebsite.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            BaseRepository baseRepository = new BaseRepository();
            List<HeaderViewModel> headerData = baseRepository.GetContentByType((int)Category.Products);

            return View(headerData);
        }

        public ActionResult Section(long MainContentId)
        {
            BaseRepository baseRepository = new BaseRepository();
            MainContentViewModel mainContentData = baseRepository.GetContentByMainContentId(MainContentId);

            return View(mainContentData);
        }

        public ActionResult SubSection(long SubContentId)
        {
            BaseRepository baseRepository = new BaseRepository();
            SubContentViewModel subContentData = baseRepository.GetContentBySubContentId(SubContentId);

            return View(subContentData);
        }
    }
}
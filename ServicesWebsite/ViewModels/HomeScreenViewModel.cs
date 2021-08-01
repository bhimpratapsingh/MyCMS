using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesWebsite.ViewModels
{
    public class HomeScreenViewModel
    {
        public List<HeaderViewModel> Services { get; set; }
        public List<HeaderViewModel> Products { get; set; }
        public List<ClientLogoViewModel> ClientLogos { get; set; }
    }
}
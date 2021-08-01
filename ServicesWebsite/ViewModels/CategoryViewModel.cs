using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesWebsite.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum Category
    {
        Services = 1,
        Products = 2
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicesWebsite.ViewModels
{
    public class MainContentViewModel
    {
        public long Id { get; set; }

        public long HeaderId { get; set; }

        public string Content { get; set; }

        public bool AddPage { get; set; }

        public string PageTitle { get; set; }

        public string Image { get; set; }

        public int SortOrder { get; set; }

        public bool MakePublic { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<SubContentViewModel> SubContentData { get; set; }

    }
}
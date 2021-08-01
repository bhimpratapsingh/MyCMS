using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicesWebsite.ViewModels
{
    public class SubContentListViewModel
    {
        public long Id { get; set; }

        public long SubContentId { get; set; }

        public string Content { get; set; }

        public int SortOrder { get; set; }

        public bool MakePublic { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
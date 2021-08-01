using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesWebsite.ViewModels
{
    public class HeaderViewModel
    {
        public long Id { get; set; }

        public string Header { get; set; }

        public string Image { get; set; }

        public int SortOrder { get; set; }

        public bool MakePublic { get; set; }

        public int Type { get; set; }

        public string BackColor { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string CategoryName { get; set; }

        public List<MainContentViewModel> MainContentData { get; set; }
        
    }
}
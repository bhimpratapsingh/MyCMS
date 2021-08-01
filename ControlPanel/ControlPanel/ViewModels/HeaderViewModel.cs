using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlPanel.ViewModels
{
    public class HeaderViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Header { get; set; }

        [Display(Name ="Image File")]
        public HttpPostedFileBase ImageFile { get; set; }

        public string Image { get; set; }

        [Display(Name ="Sort Order")]
        public int SortOrder { get; set; }

        [Display(Name = "Make Public")]
        public bool MakePublic { get; set; }

        [Required]
        public int Type { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string BackColor { get; set; }
    }
}
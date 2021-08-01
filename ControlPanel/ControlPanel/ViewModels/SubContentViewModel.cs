using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ControlPanel.ViewModels
{
    public class SubContentViewModel
    {
        public long Id { get; set; }

        public long HeaderId { get; set; }

        public long MainContentId { get; set; }

        [Required]
        public string Content { get; set; }

        [Display(Name = "Add Page")]
        public bool AddPage { get; set; }

        [Display(Name = "Page Title")]
        public string PageTitle { get; set; }

        public string Image { get; set; }

        [Display(Name = "Image File")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }

        [Display(Name = "Make Public")]
        public bool MakePublic { get; set; }

        public HttpPostedFileBase File { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ControlPanel.ViewModels
{
    public class ClientLogoViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Image { get; set; }

        [Required]
        [Display(Name="Image File")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name ="Sort Order")]
        public int SortOrder { get; set; }

        [Display(Name="Make Public")]
        public bool MakePublic { get; set; }
    }
}
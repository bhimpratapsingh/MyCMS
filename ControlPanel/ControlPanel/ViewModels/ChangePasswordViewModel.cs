using System.ComponentModel.DataAnnotations;

namespace ControlPanel.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name ="Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
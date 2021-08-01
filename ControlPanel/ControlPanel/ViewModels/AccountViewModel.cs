using System.ComponentModel.DataAnnotations;

namespace ControlPanel.ViewModels
{
    public class AccountViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
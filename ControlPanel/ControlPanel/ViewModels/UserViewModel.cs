using System;
using System.ComponentModel.DataAnnotations;

namespace ControlPanel.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool Status { get; set; }

        [Required]
        public string Designation { get; set; }

        public string Createdby { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByIp { get; set; }
        public string Modifiedby { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedByIp { get; set; }
        public string IPAddress { get; set; }
    }
}
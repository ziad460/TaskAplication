using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.Dtos
{
    public class ResetPasswordDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password, ErrorMessage = "Passwords must be at least 6 characters and one non alphanumeric character and at least one digit ('0'-'9') and at least one uppercase ('A'-'Z').")]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password, ErrorMessage = "Passwords must be at least 6 characters and one non alphanumeric character and at least one digit ('0'-'9') and at least one uppercase ('A'-'Z').")]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage =
            "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

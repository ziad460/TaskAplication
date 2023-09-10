using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "This field is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password, ErrorMessage = "Passwords must be at least 6 characters and one non alphanumeric character and at least one digit ('0'-'9') and at least one uppercase ('A'-'Z').")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}

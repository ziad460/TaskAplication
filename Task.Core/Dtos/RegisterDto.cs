using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "The Email is not a valid e-mail address")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber), Display(Name = "Phone Number"), RegularExpression("^01[0-2,5]{1}[0-9]{8}$", ErrorMessage = "The Phone Number Expression Is Invalid")]
        public string PhoneNumber { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}

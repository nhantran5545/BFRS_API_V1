using BusinessObjects.InheritanceClass.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class AccountSignUpRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        [UsernameValidation]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [PasswordValidation]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [PhoneNumberValidation]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public int? FarmId { get; set; }
    }
}

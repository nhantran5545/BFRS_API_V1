using BusinessObjects.InheritanceClass.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class FarmRequest
    {
        [Required(ErrorMessage = "Farm Name is required")]
        public string? FarmName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [PhoneNumberValidation]
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
    }
}

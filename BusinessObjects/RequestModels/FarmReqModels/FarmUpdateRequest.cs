using BusinessObjects.InheritanceClass.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.FarmReqModels
{
    public class FarmUpdateRequest
    {
        public string? FarmName { get; set; }
        public string? Address { get; set; }
        [PhoneNumberValidation]
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
    }
}

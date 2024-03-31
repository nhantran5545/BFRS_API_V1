using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class AreaAddRequest
    {
        [Required(ErrorMessage = "AreaName is required")]
        public string AreaName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "FarmId is required")]
        public int FarmId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}

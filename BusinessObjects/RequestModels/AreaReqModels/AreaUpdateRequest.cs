using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.AreaReqModels
{
    public class AreaUpdateRequest
    {
        public string AreaName { get; set; }

        public string Description { get; set; }
    }
}

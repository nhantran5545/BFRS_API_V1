using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class MutationRequest
    {
        public int MutationId { get; set; }
        public string? MutationName { get; set; }
    }
}

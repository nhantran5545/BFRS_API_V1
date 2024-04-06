using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class MutationRequest
    {
        public int BirdId { get; set; }
        public int MutationId { get; set; }
    }
}

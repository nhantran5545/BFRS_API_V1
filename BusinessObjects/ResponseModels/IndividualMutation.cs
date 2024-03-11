using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class IndividualMutation
    {
        public Guid MutationId { get; set; }
        public string? MutationName { get; set; }
    }
}

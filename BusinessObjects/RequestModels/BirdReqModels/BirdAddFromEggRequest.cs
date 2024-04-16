using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.BirdReqModels
{
    public class BirdAddFromEggRequest
    {
        public string? BirdName { get; set; }
        public string? Gender { get; set; }
        public int CageId { get; set; }
        public int? BandNumber { get; set; }
        public string? Image { get; set; }
        public string? LifeStage { get; set; }
        public string? Status { get; set; }
        public int EggId { get; set; }
        public List<MutationRequest>? MutationRequests { get; set; }
    }
}

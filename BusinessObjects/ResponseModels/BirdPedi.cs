using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class BirdPedi
    {
        public int BirdId { get; set; }
        public string? BirdName { get; set; }
        public string? Gender { get; set; }
        public int? BandNumber { get; set; }
        public string? Image { get; set; }
    }
}

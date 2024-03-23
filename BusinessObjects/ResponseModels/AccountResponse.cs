using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class AccountResponse
    {
        public string? AccountId { get; set; }
        public string AccessToken { get; set; }
        public Enums.Role Role { get; set; }

    }
}

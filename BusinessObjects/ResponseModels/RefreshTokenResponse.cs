using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class RefreshTokenResponse
    {
        public string? AccessToken { get; internal set; }
        public string? RefreshToken { get; internal set; }
    }
}

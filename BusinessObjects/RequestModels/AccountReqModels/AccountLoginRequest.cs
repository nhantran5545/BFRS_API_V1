using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.AccountReqModels
{
    public class AccountLoginRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

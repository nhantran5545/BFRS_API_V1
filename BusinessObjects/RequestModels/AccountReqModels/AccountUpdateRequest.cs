using BusinessObjects.InheritanceClass.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessObjects.RequestModels.AccountReqModels;

public class AccountUpdateRequest
{
    public int AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [PhoneNumberValidation]
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
}

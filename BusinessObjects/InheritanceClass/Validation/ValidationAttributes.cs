using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessObjects.InheritanceClass.Validation
{
    public class UsernameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var username = value as string;
            if (string.IsNullOrEmpty(username))
            {
                return new ValidationResult("Username is required.");
            }

            if (username.Length < 8 || username.Length > 50)
            {
                return new ValidationResult("Username must be between 8 and 50 characters long.");
            }

            return ValidationResult.Success;
        }
    }


    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Password is required.");
            }

            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
            if (!regex.IsMatch(password))
            {
                return new ValidationResult("Password must contain at least one uppercase letter, one lowercase letter, one digit, and be at least 8 characters long.");
            }

            return ValidationResult.Success;
        }
    }

    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var phoneNumber = value.ToString();

            // Regular expression for Vietnamese phone numbers with or without country code (+84)
            var regex = new Regex(@"^(?:\+?(84)|0)(\d{9,10})$");
            if (!regex.IsMatch(phoneNumber))
            {
                return new ValidationResult("Invalid phone number format. Example: +84912345678 or 0912345678");
            }

            return ValidationResult.Success;
        }
    }

}

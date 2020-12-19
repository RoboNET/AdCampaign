using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AdCampaign.Attributes;
using AdCampaign.DAL.Entities;

namespace AdCampaign.Models.Users
{
    public class UserEditRequest : IValidatableObject
    {
        private static Regex passwordRegex = new (@"^[A-Za-z0-9]+$");

        [Required(ErrorMessage = Constants.FieldRequired)]
        public long Id { get; set; }
        
        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Name { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        [EmailValidation]
        public string Email { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        [PhoneValidation]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = Constants.FieldRequired)]
        public Role Role { get; set; }
        
        public bool IsActive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(Password) && !passwordRegex.IsMatch(Password))
            {
                yield return new ValidationResult("Пароль должен содержать латинские буквы и цифры",new[] {nameof(Password)});
            }
        }
    }
}
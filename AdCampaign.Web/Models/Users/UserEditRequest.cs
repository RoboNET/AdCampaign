using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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
        [RegularExpression("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])", ErrorMessage = "Неверный формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Неверный формат телефона")]
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
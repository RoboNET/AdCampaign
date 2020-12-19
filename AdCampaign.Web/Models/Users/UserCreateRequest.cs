using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdCampaign.Attributes;
using AdCampaign.DAL.Entities;

namespace AdCampaign.Models.Users
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        [EmailValidation]
        public string Email { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        [PhoneValidation]
        public string Phone { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public Role Role { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Пароль должен содержать латинские буквы и цифры")]
        public string Password { get; set; }

    }
}
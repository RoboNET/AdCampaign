using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdCampaign.DAL.Entities;

namespace AdCampaign.Models.Users
{
    public class UserCreateRequest : IValidatableObject
    {
        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Email { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Phone { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public Role Role { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
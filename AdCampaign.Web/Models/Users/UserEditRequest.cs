using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdCampaign.DAL.Entities;

namespace AdCampaign.Models.Users
{
    public class UserEditRequest : IValidatableObject
    {
        [Required(ErrorMessage = Constants.FieldRequired)]
        public long Id { get; set; }
        
        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = Constants.FieldRequired)]
        public Role Role { get; set; }
        
        public bool IsActive { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
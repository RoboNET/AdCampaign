using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdCampaign.DAL.Entities;

namespace AdCampaign.Models.Users
{
    public class UserEditRequest : IValidatableObject
    {
        [Required] 
        public long Id { get; set; }
        
        [Required] 
        public string Name { get; set; }
        
        [Required] 
        public string Password { get; set; }
        
        [Required] 
        public string Email { get; set; }
        
        [Required] 
        public string Phone { get; set; }
        
        [Required] 
        public Role Role { get; set; }
        
        public bool IsActive { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
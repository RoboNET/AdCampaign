using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdCampaign.Models
{
    public class CreateApplicationViewModel : IValidatableObject
    {
        public long AdvertId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
            {
                yield return new ValidationResult(
                    $"Как минимум одно из полей должно быть заполнено.",
                    new[] { nameof(Phone), nameof(Email) });
            }
        }
    }
}
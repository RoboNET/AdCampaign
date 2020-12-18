using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdCampaign.DAL.Entities;

namespace AdCampaign.Models
{
    public class CreateApplicationViewModel : IValidatableObject
    {
        public long AdvertId { get; set; }

        [Phone(ErrorMessage = "Неверный формат телефона")]
        [RequiredIf("RequestType", RequestType.Phone, Constants.FieldRequired)]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Неверный формат электронной почты")]
        [RequiredIf("RequestType", RequestType.Email, Constants.FieldRequired)]
        public string Email { get; set; }

        public RequestType RequestType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
            {
                yield return new ValidationResult(
                    $"Как минимум одно из полей должно быть заполнено.",
                    new[] {nameof(Phone), nameof(Email)});
            }
        }
    }
}
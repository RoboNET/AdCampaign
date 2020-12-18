using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdCampaign.DAL.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AdCampaign.Models
{
    public class CreateApplicationViewModel : IValidatableObject
    {
        public long AdvertId { get; set; }

        [Phone]
        [RequiredIf("RequestType", RequestType.Phone)]
        public string Phone { get; set; }

        [EmailAddress]
        [RequiredIf("RequestType", RequestType.Email)]
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

    public class RequiredIfAttribute : ValidationAttribute, IClientModelValidator
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }


        public RequiredIfAttribute(string propertyName, object value, string errorMessage = "Field is required")
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            Value = value;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();
            var proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue != null)
            {
                if (proprtyvalue is RequestType requestTypeValue && Value is RequestType requestValue)
                {
                    if (requestValue.HasFlag(requestTypeValue) && value==null)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
                else if (proprtyvalue.ToString() == Value.ToString() && value == null)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }


        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-vatNumber", ErrorMessage);
            context.Attributes.Add("data-val-vatNumber-businessType", Value.ToString());
        }
    }

}
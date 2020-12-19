using System.ComponentModel.DataAnnotations;

namespace AdCampaign.Attributes
{
    public class PhoneValidationAttribute : RegularExpressionAttribute
    {
        private const string Pattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        private const string DefaultErrorMessage = "Неверный формат телефона";

        public PhoneValidationAttribute() : base(Pattern)
        {
            ErrorMessage = DefaultErrorMessage;
        }
    }
}
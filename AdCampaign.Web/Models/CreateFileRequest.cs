using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdCampaign.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace AdCampaign.Models
{
    public class UpdateFileRequestModel : BaseFileRequestModel
    {
        [Required(ErrorMessage = Constants.FieldRequired)]
        public long Id { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public bool IsVisible { get; set; }

        public IFormFile PrimaryImage { get; set; }

        public IFormFile SecondaryImage { get; set; }
    }

    public class BaseFileRequestModel : IValidatableObject
    {
        [Required(ErrorMessage = Constants.FieldRequired)]
        public bool ImpressingAlways { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public RequestType RequestType { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public DateTime ImpressingDateFrom { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public DateTime ImpressingDateTo { get; set; }

        public TimeSpan? ImpressingTimeFrom { get; set; }

        public TimeSpan? ImpressingTimeTo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ImpressingAlways && (!ImpressingTimeFrom.HasValue || !ImpressingTimeTo.HasValue))
            {
                yield return new ValidationResult(
                    $"Поскольку реклама отображается не всегда, должно быть установлено время показа.",
                    new[] {nameof(ImpressingTimeFrom), nameof(ImpressingTimeTo)});
            }
        }
    }

    public class CreateFileRequestModel : BaseFileRequestModel
    {
        [Required(ErrorMessage = Constants.FieldRequired)]
        public IFormFile PrimaryImage { get; set; }

        [Required(ErrorMessage = Constants.FieldRequired)]
        public IFormFile SecondaryImage { get; set; }
    }
}
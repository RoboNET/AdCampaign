using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdCampaign.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace AdCampaign.Models
{
    public class UpdateFileRequestModel : BaseFileRequestModel
    {
        [Required] public long Id { get; set; }

        [Required] public bool IsVisible { get; set; }

        public IFormFile PrimaryImage { get; set; }

        public IFormFile SecondaryImage { get; set; }
    }

    public class BaseFileRequestModel : IValidatableObject
    {
        [Required] public bool ImpressingAlways { get; set; }
        
        [Required] public string Name { get; set; }

        [Required] public RequestType RequestType { get; set; }

        [Required] public DateTime ImpressingDateFrom { get; set; }

        [Required] public DateTime ImpressingDateTo { get; set; }

        public TimeSpan? ImpressingTimeFrom { get; set; }

        public TimeSpan? ImpressingTimeTo { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ImpressingAlways && (!ImpressingTimeFrom.HasValue || !ImpressingTimeTo.HasValue))
            {
                yield return new ValidationResult(
                    $"Поскольку реклама отображается не всегда, должно быть установлено время показа.",
                    new[] { nameof(ImpressingTimeFrom), nameof(ImpressingTimeTo) });
            }
        }
    }

    public class CreateFileRequestModel : BaseFileRequestModel
    {
        [Required] public IFormFile PrimaryImage { get; set; }

        [Required] public IFormFile SecondaryImage { get; set; }
    }
}
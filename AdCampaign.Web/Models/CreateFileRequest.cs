using System;
using AdCampaign.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace AdCampaign.Models
{
    public class CreateFileRequestModel
    {
        public string Name { get; set; }

        public RequestType RequestType { get; set; }
        
        public bool IsVisible { get; set; }
        
        public DateTime ImpressingDateFrom { get; set; }

        public DateTime ImpressingDateTo { get; set; }

        public TimeSpan ImpressingTimeFrom { get; set; }

        public TimeSpan ImpressingTimeTo { get; set; }

        public IFormFile PrimaryImage { get; set; }
        
        public IFormFile SecondaryImage { get; set; }

    }
}
﻿using AdCampaign.DAL.Entities;

namespace AdCampaign.BLL.Services.Adverts.DTO
{
    /// <summary>
    ///  Кампания к показу
    /// </summary>
    public class ShowAdvertDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public RequestType RequestType { get; set; }
        
        //todo add photo here

        //todo automapper   
        public ShowAdvertDto(Advert advert)
        {
            Id = advert.Id;
            Name = advert.Name;
            RequestType = advert.RequestType;
        }
    }
}
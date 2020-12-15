using System;

namespace AdCampaign.DAL.Entities
{
    [Flags]
    public enum RequestType
    {
        Phone = 1,
        Email = 2
    }
}
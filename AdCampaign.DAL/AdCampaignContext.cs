using System;
using Microsoft.EntityFrameworkCore;

namespace AdCampaign.DAL
{
    public class AdCampaignContext : DbContext
    {
        public AdCampaignContext(DbContextOptions<AdCampaignContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
using System;
using AdCampaign.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdCampaign.DAL
{
    public class AdCampaignContext : DbContext
    {
        public AdCampaignContext(DbContextOptions<AdCampaignContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<AdvertStatistic> AdvertsStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(user => user.Adverts).WithOne(advert => advert.Owner);
            modelBuilder.Entity<User>().HasOne(user => user.BlockedBy).WithOne();

            modelBuilder.Entity<Advert>().HasMany(advert => advert.AdvertStatistics)
                .WithOne(statistic => statistic.Advert);
            modelBuilder.Entity<Advert>().HasMany(advert => advert.Applications)
                .WithOne(application => application.Advert);
            modelBuilder.Entity<Advert>().HasOne(advert => advert.BlockedBy)
                .WithOne();

            base.OnModelCreating(modelBuilder);
        }
    }
}
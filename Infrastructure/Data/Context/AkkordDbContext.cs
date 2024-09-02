using Application.Entities;
using Application.Entities.Security;
using Infrastructure.Data.EntityMappings;
using Infrastructure.Data.EntityMappings.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Context
{
    public class AkkordDbContext : DbContext
    {
        // akkord
        public virtual DbSet<ProductPortfolio> ProductPortfolios { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<SaleManager> SaleManagers { get; set; }
        public virtual DbSet<SaleManagerRegion> SaleManagerRegions { get; set; }
        public virtual DbSet<SalePoint> SalePoint { get; set; }
        public virtual DbSet<SalePointPortfolio> SalePointPortfolios { get; set; }
        public virtual DbSet<SalePointSeller> SalePointSellers { get; set; }

        // security
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public AkkordDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // akkord
            builder.ApplyConfiguration(new PhotoMapping());
            builder.ApplyConfiguration(new ProductPortfolioMapping());
            builder.ApplyConfiguration(new RegionMapping());
            builder.ApplyConfiguration(new SaleManagerMapping());
            builder.ApplyConfiguration(new SaleManagerRegionMapping());
            builder.ApplyConfiguration(new SalePointMapping());
            builder.ApplyConfiguration(new SalePointPortfolioMapping());
            builder.ApplyConfiguration(new SalePointSellerMapping());
            builder.ApplyConfiguration(new SalePointPhotoMapping());

            builder.ApplyConfiguration(new CrmTaskMapping());
            //builder.ApplyConfiguration(new CrmTaskAssignmentMapping());
            builder.ApplyConfiguration(new CrmTaskPhotoMapping());
            builder.ApplyConfiguration(new CityOfBakuMapping());
            builder.ApplyConfiguration(new NotificationMapping());

            // security
            builder.ApplyConfiguration(new UserMapping());
            builder.ApplyConfiguration(new RoleMapping());
            builder.ApplyConfiguration(new UserRoleMapping());

            base.OnModelCreating(builder); // if we put this at top, it changes also identity table names so dont 

            AkkordContextSeed.Seed(builder);
        }
    }
}

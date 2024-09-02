using Application.Constants;
using Application.Entities;
using Application.Entities.Security;
using Application.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Context
{
    public class AkkordContextSeed
    {
        public static void Seed(ModelBuilder builder)
        {
            // In regions Baku id must be 99

            var roles = new List<Role>
            {
                new Role { Id = 1, Name = RoleConstant.Admin },
                new Role { Id = 2, Name = RoleConstant.SaleManager }
            };

            var adminId = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482");
            //var userId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7");
            //var userId2 = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");
           

            //var regions = new List<Region>
            //{
            //    new Region { Id = 99, Name = "Baki" }
            //};

            //var portfolioes = new List<ProductPortfolio>
            //{
            //    new ProductPortfolio { Id = 1, Name = "Norm" },
            //    new ProductPortfolio { Id = 2, Name = "Holcim" },
            //    new ProductPortfolio { Id = 3, Name = "Akkord" }
            //};

            var users = new List<User>
            {
                new User  { 
                    Id = adminId,
                    Username = "admin",
                    Password = "admin"
                },
                //new User {
                //    Id = userId,
                //    Username = "retail",
                //    Password = "retail"
                //},

                //new User {
                //    Id = userId2,
                //    Username = "corporate",
                //    Password = "corporate"
                //}
            };

        //    var userRoles = new List<UserRole>
        //    {
        //        new UserRole { UserId = adminId, RoleId = 1 },
        //        new UserRole { UserId = userId, RoleId = 2 },
        //        new UserRole { UserId = userId2, RoleId = 2 }
        //    };

        //    var saleManagers = new List<SaleManager>
        //    {
        //        new SaleManager
        //        {
        //            Id = userId,
        //            FirstName = "Mahir",
        //            LastName = "Abbasov",
        //            Msisdn = "99450123456",
        //            SaleSegment = SaleSegment.Retail,
        //            Status = ActivationStatus.Active
        //        },
        //        new SaleManager
        //        {
        //            Id = userId2,
        //            FirstName = "Namiq",
        //            LastName = "Quliyev",
        //            Msisdn = "994774443322",
        //            SaleSegment = SaleSegment.Corporate,
        //            Status = ActivationStatus.Active
        //}
        //    };

        //    var saleManagerRegions = new List<SaleManagerRegion>
        //    {
        //        new SaleManagerRegion { RegionId = 1, SaleManagerId = userId },
        //        new SaleManagerRegion { RegionId = 4, SaleManagerId = userId },

        //        new SaleManagerRegion { RegionId = 2, SaleManagerId = userId2 },
        //        new SaleManagerRegion { RegionId = 3, SaleManagerId = userId2 }
        //    };

        //    var photoes = new List<Photo>
        //    {
        //        new Photo 
        //        {
        //            Id = 1,
        //            Src = @"C:\Photo\retail.jpg"
        //        },
        //        new Photo
        //        {
        //            Id = 2,
        //            Src = @"C:\Photo\corporate.jpg"
        //        }
        //    };

        //    var salePoints = new List<SalePoint>
        //    {
        //        new SalePoint
        //        {
        //            Id = 1,
        //            Address = "Quba seheri",
        //            CodeName = "Ramiz Quba Akkord",
        //            ContactPerson = "Vuqar Alizade",
        //            ContactPersonDateOfBirth = DateTime.Now.AddYears(-33),
        //            ContactPersonMsisdn = "994519876543",
        //            District = "Mehmandarov street",
        //            GpsX = 1234,
        //            GpsY = 72,
        //            JuridicalName = "Juridical Rauf Quliyev",
        //            OwnerDateOfBirth = DateTime.Now.AddDays(45).AddYears(-28),
        //            OwnerMsisdn = "994709002121",
        //            OwnerOrDirector = "Qurban Velizade",
        //            RegionId = 1,
        //            PhotoId = 1,
        //            SaleManagerId = userId,
        //            ShopType = ShopType.Single,
        //            ShopAgg = 1000,
        //            ShopCement = 2300,
        //            Village = "",
        //            ShopName = "Shop Quba",
        //            ShopClassification = ShopClassification.BuildingAndOpenSpace,
        //            ShopOwnership = ShopOwnership.Own,
        //            Comment = "comment",
        //            SaleSegment = SaleSegment.Retail,
        //            City = "Quba",
        //        },
        //        new SalePoint
        //        {
        //            Id = 2,
        //            Address = "Qazax seheri",
        //            CodeName = "Medet  Sement",
        //            ContactPerson = "Arif Esedoqlu",
        //            ContactPersonDateOfBirth = DateTime.Now.AddYears(-42),
        //            ContactPersonMsisdn = "994501006543",
        //            District = "City point street",
        //            GpsX = 1234,
        //            GpsY = 72,
        //            JuridicalName = "Mehman Muradov",
        //            OwnerDateOfBirth = DateTime.Now.AddDays(22).AddYears(-36),
        //            OwnerMsisdn = "994103114451",
        //            OwnerOrDirector = "Vaqif Ilyasov",
        //            RegionId = 2,
        //            PhotoId = 2,
        //            SaleManagerId = userId2,
                   
        //            ShopAgg = 3000,
        //            ShopCement = 5000,
        //            Village = "",
        //            // ShopName = "Shop Quba",
        //            // ShopType = ShopType.Single,
        //            // ShopClassification = ShopClassification.BuildingAndOpenSpace,
        //            ShopOwnership = ShopOwnership.Own,
        //            Comment = "comment",
        //            SaleSegment = SaleSegment.Corporate,
        //            City = "Qazax",

        //            CorporateSegment = CorporateSegment.Construction,
        //            ConstructionType = ConstructionType.Residental,
        //            ConstBetonSupplier = "BVK MMC",
                    
                    
        //        }
        //    };

        //    var salePointPortfolioes = new List<SalePointPortfolio>
        //    {
        //        new SalePointPortfolio { SalePointId = 1, PortfolioId = 1 },
        //        new SalePointPortfolio { SalePointId = 1, PortfolioId = 3 },

        //        new SalePointPortfolio { SalePointId = 2, PortfolioId = 2 },
        //        new SalePointPortfolio { SalePointId = 2, PortfolioId = 3 }
        //    };

        //    var salePointSellers = new List<SalePointSeller>
        //    {
        //        new SalePointSeller { 
        //            Id = 1, 
        //            DateOfBirth = DateTime.Now.AddDays(23).AddYears(-20),
        //            Fullname = "Maqsud Ibrahimov", 
        //            Msisdn = "994559998877",
        //            SalePointId = 1
        //        },
        //        new SalePointSeller {
        //            Id = 2,
        //            DateOfBirth = DateTime.Now.AddDays(54).AddYears(-29),
        //            Fullname = "Mehemmed Quluzade",
        //            Msisdn = "994773434343",
        //            SalePointId = 1
        //        },
        //    };

            // security
            builder.Entity<Role>().HasData(roles);
            builder.Entity<User>().HasData(users);
            //builder.Entity<UserRole>().HasData(userRoles);

            //// akkord
            //builder.Entity<Region>().HasData(regions);
            //builder.Entity<ProductPortfolio>().HasData(portfolioes);
            //builder.Entity<SaleManager>().HasData(saleManagers);
            //builder.Entity<SaleManagerRegion>().HasData(saleManagerRegions);
            //builder.Entity<Photo>().HasData(photoes);
            //builder.Entity<SalePoint>().HasData(salePoints);
            //builder.Entity<SalePointPortfolio>().HasData(salePointPortfolioes);
            //builder.Entity<SalePointSeller>().HasData(salePointSellers);

        }
    }
}

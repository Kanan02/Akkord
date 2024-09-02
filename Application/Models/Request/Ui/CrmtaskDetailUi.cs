using Application.Config;
using Application.Entities;
using Application.Enums;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Request.Ui
{
    public class CrmtaskDetailUi
    {
        public int Id { get; set; }
        public DateTime? VisitDt { get; set; }
        public int? RegionId { get; set; }
        public string City { get; set; }
        public string CodeName { get; set; }
        public int SalePointId { get; set; }
        public PurposeOfVisit PurposeOfVisit { get; set; }
        public string MeetingResult { get; set; }
        public CorporateSegment CorporateSegment { get; set; }
        public CrmTaskStatus Status { get; set; }
        // if CorporateSegment = Construction
        public string BetonSupplier { get; set; }
        public List<int> Portfolios { get; set; }
        public List<PhotoDto> Photos { get; set; }
        public decimal? GpsX { get; set; }
        public decimal? GpsY { get; set; }
        public double? CurrGpsX { get; set; }
        public double? CurrGpsY { get; set; }
        public decimal ShopCement { get; set; }
        public SalePointStatus SalePointStatus { get; set; }

        public CrmtaskDetailUi() { }

        public CrmtaskDetailUi(CrmTask task)
        {
            Id = task.Id;
            VisitDt = task.VisitDt;
            RegionId = task.SalePoint?.Region?.Id;
            City = task.SalePoint?.City;
            CodeName = task.SalePoint?.CodeName;
            SalePointId = task.SalePointId;
            PurposeOfVisit = task.PurposeOfVisit;
            MeetingResult = task.MeetingResult;
            CorporateSegment = task.SalePoint.CorporateSegment;
            BetonSupplier = task.SalePoint?.ConstBetonSupplier;
            GpsX = task.SalePoint?.GpsX;
            GpsY = task.SalePoint?.GpsY;
            ShopCement = task.SalePoint?.ShopCement ?? 0;
            Status = task.Status;
            Portfolios = task.SalePoint?.SalePointPortfolioes != null
                         && task.SalePoint?.SalePointPortfolioes.Count > 0
                ? task.SalePoint?.SalePointPortfolioes.Select(x => x.PortfolioId).ToList()
                : new List<int>();
            Photos = task.Photos != null && task.Photos.Count() > 0
                ? task.Photos.Select(p => new PhotoDto(p.Photo, ProjectSetting.PhotoPath)).ToList()
                : new List<PhotoDto>();
        }
    }
}

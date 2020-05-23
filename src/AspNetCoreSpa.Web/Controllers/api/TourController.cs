using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using AngleSharp.Common;
using AspNetCoreSpa.Core;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreSpa.Web.Controllers.api
{
    public class TourController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public TourController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/Tour
        [HttpGet]
        public IActionResult Get()
        {
            if (User.IsInRole("Admin")||User.IsInRole("admin")||User.IsInRole("Staff")||User.IsInRole("staff"))
            {
                IEnumerable<Tour> allTours=null; 
                if (User.IsInRole("Admin")||User.IsInRole("admin"))
                {
                     
                     allTours = _uow.Tours.GetAll().Where(x => x.Censorship && !x.Deleted && x.Status);
                }
                else
                {
                    allTours = _uow.Tours.GetAll().Where(x => !x.Deleted && x.Status);  
                }
                foreach (var allTour in allTours )
                {
                    var prices = _uow.Prices.Find(x => x.TourId == allTour.Id).OrderBy(o => o.TouristType).ToList();
                    allTour.Prices = prices;
                    var tourproData = _uow.TourPrograms.Find(x => x.TourId == allTour.Id).ToList();
                    allTour.TourPrograms = tourproData;
                }

                var allToursVm = _mapper.Map<IEnumerable<TourVM>>(allTours);
                foreach (var tourVm in allToursVm)
                {
                    var tourCategory = _uow.TourCategories.GetSingleOrDefault(x=>x.Id==tourVm.TourCategoryId);
                    if (tourCategory!=null)
                    {
                        tourVm.CategoryName = tourCategory.Name;
                    }
                    var departureName = _uow.Provinces.GetSingleOrDefault(x=>x.Id==tourVm.DepartureId);
                    if (departureName!=null)
                    {
                        tourVm.DepartureName = departureName.Name;
                    }
                }
                
                return Ok(allToursVm);
            }
            else
            {
                var allTour = _uow.Tours.GetAll();
                allTour = allTour.Where(x => x.Censorship && !x.Deleted && x.Status);
                return Ok(_mapper.Map<IEnumerable<TourVM>>(allTour));
            }
            
        }
        [HttpGet("Search")]
        public IActionResult Get(string departureId,string destinationId,string departureDateTimeStamp,string tourCategoryId,int priceId)
        {
            var departureDate = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(departureDateTimeStamp));
            var conditionCate = tourCategoryId != "0";
            var conditionDestination=destinationId != "0";
            var conditionPrice=priceId != 0;
            decimal startPriceCondition = (decimal) 0.00;
            decimal endPriceCondition = (decimal) 1000000000.00;
            if (conditionPrice)
            {
                switch (priceId)
                {
                    case 1:
                        endPriceCondition = (decimal) 99.99;
                        break;
                    case 2:
                        startPriceCondition = (decimal)100.00;
                        endPriceCondition = (decimal)300.00;
                        break;
                    case 3:
                        startPriceCondition = (decimal)300.00;
                        endPriceCondition = (decimal)600.00;
                        break;
                    case 4:
                        startPriceCondition = (decimal)600.00;
                        endPriceCondition = (decimal)800.00;
                        break;
                    case 5:
                        startPriceCondition = (decimal)800.00;
                        endPriceCondition = (decimal)1000.00;
                        break;
                    case 6:
                        startPriceCondition = (decimal)1000.00;
                        break;
                } 
            }
            
            var allTour = (from tour in _uow.Tours
                join price in _uow.Prices
                    on tour.Id equals price.TourId
                join tourCate in _uow.TourCategories
                    on tour.TourCategoryId equals tourCate.Id
                where (price.TouristType == TouristTypeEnum.Adult.ToTouristTypeInt()&&tour.Slot>0&&tour.Deleted==false&&tour.Censorship&&tour.Status
                      &&(!conditionCate || tourCate.Id==Guid.Parse(tourCategoryId))
                      &&(!conditionDestination || tour.DestinationId==Guid.Parse(destinationId))
                      &&price.PromotionPrice>=startPriceCondition
                      &&price.PromotionPrice<=endPriceCondition
                      && tour.DepartureId==Guid.Parse(departureId))
                      ||(tour.DepartureId==Guid.Parse(departureId)
                         &&(!conditionCate || tourCate.Id==Guid.Parse(tourCategoryId))
                         &&(!conditionDestination || tour.DestinationId==Guid.Parse(destinationId))
                         &&price.PromotionPrice>=startPriceCondition
                         &&price.PromotionPrice<=endPriceCondition
                         &&departureDate.Day==tour.DepartureDate.Day
                          &&departureDate.Month==tour.DepartureDate.Month
                         &&departureDate.Year==tour.DepartureDate.Year
            &&price.TouristType == TouristTypeEnum.Adult.ToTouristTypeInt()&&tour.Slot>0&&tour.Deleted==false&&tour.Censorship&&tour.Status)
                     
                select new
                    TourCardVM()
                    {
                        Id = tour.Id,
                        Description = tour.Description,
                        DepartureDate = tour.DepartureDate,
                        DepartureId = tour.DepartureId,
                        Image = tour.Image,
                        Name = tour.Name,
                        Slot = tour.Slot,
                        ViewCount = tour.ViewCount,
                        OriginalPrice = price.OriginalPrice,
                        PromotionPrice = price.PromotionPrice,
                        StartDatePro = price.StartDatePro,
                        EndDatePro=price.EndDatePro,
                        TourCategoryId = tour.TourCategoryId
                            
                    });
            return Ok(allTour);
        }
        // GET: api/Tour/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            
            var tour = User.IsInRole("Admin")||User.IsInRole("admin") ?  _uow.Tours.Get(id):_uow.Tours.SingleOrDefault(x => x.Id == id&&x.Censorship&&x.Status&&!x.Deleted);
            if(tour==null)
            {
                    
                var modelState = new ModelStateDictionary();
                modelState.AddModelError("message", "errors found. Can't get this tour");
                return BadRequest(modelState);
            }
            var tourCate = _uow.TourCategories.Get(tour.TourCategoryId);
            var province = _uow.Provinces.Get(tour.DepartureId);
            var province2 = _uow.Provinces.Get(tour.DestinationId);

            var price = _uow.Prices
                .Find(x => x.TourId == tour.Id && x.TouristType == TouristTypeEnum.Adult.ToTouristTypeInt())
                .SingleOrDefault();
            var tourPrograms = _uow.TourPrograms.Find(x => x.TourId == id).OrderBy(o => o.OrderNumber).ToList();
            var prices = _uow.Prices.Find(x => x.TourId == id).OrderBy(o => o.TouristType).ToList();
            tour.TourPrograms = tourPrograms;
            tour.Prices = prices;
            var tourVM = _mapper.Map<TourVM>(tour);
            tourVM.CategoryName = tourCate.Name;
            tourVM.DepartureName = province.Name;
            tourVM.PromotionPrice = price.PromotionPrice;
            tourVM.OriginalPrice = price.OriginalPrice;
            tourVM.StartDatePro = price.StartDatePro;
            tourVM.EndDatePro = price.EndDatePro;
            return Ok(tourVM);
        }
        [HttpGet("hotest")]
        public IActionResult GetHotest()
        {
            var allTour = (from tour in _uow.Tours
                join price in _uow.Prices
                    on tour.Id equals price.TourId
                join tourCate in _uow.TourCategories
                    on tour.TourCategoryId equals tourCate.Id
                join tourBooking in _uow.TourBookings on tour.Id equals tourBooking.TourId 
                where price.TouristType == TouristTypeEnum.Adult.ToTouristTypeInt()&&tour.Slot>0&&tour.Deleted==false&&tour.Censorship&&tour.Status
                        select new
                            TourCardVM()
                        {
                            Id = tour.Id,
                            Description = tour.Description,
                            DepartureDate = tour.DepartureDate,
                            DepartureId = tour.DepartureId,
                            Image = tour.Image,
                            Name = tour.Name,
                            Slot = tour.Slot,
                            ViewCount = tour.ViewCount,
                            OriginalPrice = price.OriginalPrice,
                            PromotionPrice = price.PromotionPrice,
                            StartDatePro = price.StartDatePro,
                            EndDatePro = price.EndDatePro,
                            TourCategoryId = tour.TourCategoryId
                        }).Take(8);
            var toursGrouped = allTour.GroupBy(n => n.Id).
                Select(group =>
                    new
                    {
                        TourId = group.Key,
                        Tours = group.ToList(),
                        Count = group.Count()
                    }).OrderByDescending(x=>x.Count).ThenBy(x=>x.Tours[0].Name);
            return Ok(toursGrouped);
        }
        [HttpGet("newest")]
        public IActionResult GetNewest()
        {
            var allTour = (from tour in _uow.Tours
                join price in _uow.Prices
                    on tour.Id equals price.TourId
                join tourCate in _uow.TourCategories
                    on tour.TourCategoryId equals tourCate.Id
                where price.TouristType == TouristTypeEnum.Adult.ToTouristTypeInt()&&tour.Slot>0&&tour.Deleted==false&&tour.Censorship&&tour.Status
                select new
                    TourCardVM()
                    {
                        Id = tour.Id,
                        Description = tour.Description,
                        DepartureDate = tour.DepartureDate,
                        DepartureId = tour.DepartureId,
                        Image = tour.Image,
                        Name = tour.Name,
                        Slot = tour.Slot,
                        ViewCount = tour.ViewCount,
                        CreatedAt = tour.CreatedAt,
                            OriginalPrice = price.OriginalPrice,
                            PromotionPrice = price.PromotionPrice,
                            StartDatePro = price.StartDatePro,
                    EndDatePro = price.EndDatePro,
                    TourCategoryId = tour.TourCategoryId
                    }).OrderByDescending(x=>x.CreatedAt).Take(8);
            return Ok(allTour);
        }
        [HttpGet("toursSameCate/{id}")]
        public IActionResult GetToursSameCategory(Guid Id)
        {
            var tourE = _uow.Tours.Get(Id);
            var tourCategoryE = _uow.TourCategories.Get(tourE.TourCategoryId);
            var allTour = (from tour in _uow.Tours
                join price in _uow.Prices
                    on tour.Id equals price.TourId
                join tourCate in _uow.TourCategories
                    on tour.TourCategoryId equals tourCate.Id
                where price.TouristType == TouristTypeEnum.Adult.ToTouristTypeInt()&&tour.Slot>0&&tour.Deleted==false&&tour.Status&&tourCate.Id==tourCategoryE.Id&&tour.Id!=Id&&tour.Censorship
                select new
                    TourCardVM()
                    {
                        Id = tour.Id,
                        Description = tour.Description,
                        DepartureDate = tour.DepartureDate,
                        DepartureId = tour.DepartureId,
                        Image = tour.Image,
                        Name = tour.Name,
                        Slot = tour.Slot,
                        ViewCount = tour.ViewCount,
                        OriginalPrice = price.OriginalPrice,
                        PromotionPrice = price.PromotionPrice,
                        StartDatePro = price.StartDatePro,
                    EndDatePro = price.EndDatePro,
                    TourCategoryId = tour.TourCategoryId
                            
                    }).OrderByDescending(x=>x.DepartureDate).Take(4);
            if (!allTour.Any())
            {
                allTour = (from tour in _uow.Tours
                    join price in _uow.Prices
                        on tour.Id equals price.TourId
                    join tourCate in _uow.TourCategories
                        on tour.TourCategoryId equals tourCate.Id
                    where price.TouristType == TouristTypeEnum.Adult.ToTouristTypeInt()&&tour.Slot>0&&tour.Deleted==false&&tour.Id!=Id
                    select new
                        TourCardVM()
                        {
                            Id = tour.Id,
                            Description = tour.Description,
                            DepartureDate = tour.DepartureDate,
                            DepartureId = tour.DepartureId,
                            Image = tour.Image,
                            Name = tour.Name,
                            Slot = tour.Slot,
                            ViewCount = tour.ViewCount,
                            OriginalPrice = price.OriginalPrice,
                            PromotionPrice = price.PromotionPrice,
                            StartDatePro = price.StartDatePro,
                        EndDatePro = price.EndDatePro,
                        TourCategoryId = tour.TourCategoryId
                            
                        }).OrderByDescending(x=>x.DepartureDate).Take(4);
            }
            return Ok(allTour);
        }
        [HttpGet("toursByCateId/{id}")]
        public IActionResult GetToursByCateId(Guid id)
        {
            var allTour = (from tour in _uow.Tours
                join price in _uow.Prices
                    on tour.Id equals price.TourId
                join tourCate in _uow.TourCategories
                    on tour.TourCategoryId equals tourCate.Id
                where price.TouristType == TouristTypeEnum.Adult.ToTouristTypeInt()&&tour.Deleted==false&&tourCate.Id==id&&tour.Censorship
                select new
                    TourCardVM()
                    {
                        Id = tour.Id,
                        Description = tour.Description,
                        DepartureDate = tour.DepartureDate,
                        DepartureId = tour.DepartureId,
                        Image = tour.Image,
                        Name = tour.Name,
                        Slot = tour.Slot,
                        ViewCount = tour.ViewCount,
                        OriginalPrice = price.OriginalPrice,
                        PromotionPrice = price.PromotionPrice,
                        StartDatePro = price.StartDatePro,
                    EndDatePro = price.EndDatePro,
                    TourCategoryId = tour.TourCategoryId
                            
                    }).OrderByDescending(x=>x.DepartureDate);
            return Ok(allTour);
        }
        // POST: api/Tour
        [HttpPost]
        public void Post([FromBody] TourVM tour)
        {
            tour.Status = true;
            _uow.Tours.Add(_mapper.Map<Tour>(tour));
            _uow.SaveChanges();
        }

        // PUT: api/Tour/5
        [HttpPut("{id}")]
        [Authorize(Roles = ("admin,Admin,staff,Staff"))]
        public void Put(Guid id, [FromBody] TourVM tour)
        {
            var t = _uow.Tours.Get(id);
            t.Name = tour.Name;
            t.Image = tour.Image;
            t.Images = tour.Images;
            t.Description = tour.Description;
            t.DepartureDate = tour.DepartureDate;
            t.DepartureId = tour.DepartureId;
            t.DestinationId = tour.DestinationId;
            t.Slot = tour.Slot;
            t.ViewCount = tour.ViewCount;
            t.TourCategoryId = tour.TourCategoryId;
            _uow.Tours.Update(t);
            var result = _uow.SaveChanges();
            
        }
        // PUT: api/Tour/IncreaseViewCount/{id}
        [HttpPut("IncreaseViewCount/{id}")]
        public void Put(Guid id)
        {
            var t = _uow.Tours.Get(id);
            t.ViewCount = t.ViewCount+1;
            _uow.Tours.Update(t);
            var result = _uow.SaveChanges();
        }
        // DELETE: api/Tour/5
        [HttpDelete("{id}")]
        [Authorize(Roles = ("admin,Admin,staff,Staff"))]
        public void Delete(Guid id)
        {
            _uow.Tours.Remove(_uow.Tours.Get(id));
            _uow.SaveChanges();
        }
        // GET: api/tour/cencershiptour
        [HttpGet("cencershiptour")]
        public IActionResult GetCencershiptour()
        {
            var allTours = _uow.Tours.GetAll().Where(x => !x.Censorship);
            foreach (var allTour in allTours )
            {
                var prices = _uow.Prices.Find(x => x.TourId == allTour.Id).OrderBy(o => o.TouristType).ToList();
                allTour.Prices = prices;
                var tourproData = _uow.TourPrograms.Find(x => x.TourId == allTour.Id).ToList();
                allTour.TourPrograms = tourproData;
            }

            var allToursVm = _mapper.Map<IEnumerable<TourVM>>(allTours);
            foreach (var tourVm in allToursVm)
            {
                var tourCategory = _uow.TourCategories.GetSingleOrDefault(x=>x.Id==tourVm.TourCategoryId);
                if (tourCategory!=null)
                {
                    tourVm.CategoryName = tourCategory.Name;
                }
                var departureName = _uow.Provinces.GetSingleOrDefault(x=>x.Id==tourVm.DepartureId);
                if (departureName!=null)
                {
                    tourVm.DepartureName = departureName.Name;
                }
            }
                
            return Ok(allToursVm);
        }
        // PUT: api/tour/DeleteImage/{id}
        [HttpPut("DeleteImage/{id}")]
        [Authorize(Roles = ("admin,Admin,staff,Staff"))]
        public void DeleteImage(Guid id,[FromBody] TourImageVM imageVM)
        {
            var t = _uow.Tours.Get(id);
            var images = !string.IsNullOrEmpty(t.Images)?t.Images.Split("|").ToList():new List<string>();
            if (images.Count > 0)
            {
               images.Remove(imageVM.image);
            }

            string imagesStr = "";
            for (int i = 0; i < images.Count; i++)
            {
                if (i <images.Count-1)
                {
                    imagesStr += images.GetItemByIndex(i) + "|";
                }
                else
                {
                    imagesStr += images.GetItemByIndex(i); 
                }
            }

            t.Images = imagesStr;
            _uow.Tours.Update(t);
            var result = _uow.SaveChanges();
        }
        // PUT: api/tour/AddImage/{id}
        [HttpPut("AddImage/{id}")]
        [Authorize(Roles = ("admin,Admin,staff,Staff"))]
        public void AddImage(Guid id,[FromBody] TourImageVM imageVM)
        {
            var t = _uow.Tours.Get(id);
            if (!string.IsNullOrEmpty(t.Images)&&t.Images.Split("|").Length > 0)
            {
                t.Images = t.Images+"|"+imageVM.image; 
            }
            else
            {
                t.Images = imageVM.image; 
            }
            _uow.Tours.Update(t);
            var result = _uow.SaveChanges();
        }
        // PUT: api/tour/accepttour/{id}
        [HttpPut("accepttour/{id}")]
        public void AcceptTour(Guid id)
        {
            var t = _uow.Tours.Get(id);
            t.Censorship = true;
            t.Status = true;
            _uow.Tours.Update(t);
            var result = _uow.SaveChanges();
        }
        // PUT: api/tour/statustour/{id}
        [HttpPut("statustour/{id}")]
        public void UpdateStatus(Guid id)
        {
            var t = _uow.Tours.Get(id);
            t.Status = false;
            _uow.Tours.Update(t);
            var result = _uow.SaveChanges();
        }
        // PUT: api/tour/uploadImage
        [HttpPost("UploadImage")]
        [DisableRequestSizeLimit]
        [Authorize(Roles = ("admin,Admin,staff,Staff"))]
        public IActionResult UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("ClientApp", "src","assets","images","tours");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
 
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
 
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
 
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
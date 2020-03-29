using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreSpa.Core;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            var allTour = _uow.Tours.GetAll();
            return Ok(_mapper.Map<IEnumerable<TourVM>>(allTour));
        }

        // GET: api/Tour/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var tour = _uow.Tours.Get(id);
            return Ok(_mapper.Map<TourVM>(tour));
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
                where price.TouristTypeId == TouristTypeEnum.Adult.ToTouristTypeInt()
                        select new
                            TourNewestVM()
                        {
                            Id = tour.Id,
                            Description = tour.Description,
                            DepartureDate = tour.DepartureDate,
                            DepartureId = tour.DepartureId,
                            Image = tour.Image,
                            Name = tour.Name,
                            Slot = tour.Slot,
                            OriginalPrice = price.OriginalPrice,
                            PromotionPrice = price.PromotionPrice,
                            StartDatePro = price.StartDatePro,
                            TouristTypeId = price.TouristTypeId
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
                where price.TouristTypeId == TouristTypeEnum.Adult.ToTouristTypeInt()
                select new
                    TourNewestVM()
                    {
                        Id = tour.Id,
                        Description = tour.Description,
                        DepartureDate = tour.DepartureDate,
                        DepartureId = tour.DepartureId,
                        Image = tour.Image,
                        Name = tour.Name,
                        Slot = tour.Slot,
                            OriginalPrice = price.OriginalPrice,
                            PromotionPrice = price.PromotionPrice,
                            StartDatePro = price.StartDatePro,
                            TouristTypeId = price.TouristTypeId
                            
                    }).OrderByDescending(x=>x.DepartureDate).Take(8);
            return Ok(allTour);
        }
        // POST: api/Tour
        [HttpPost]
        public void Post([FromBody] TourVM tour)
        {
            _uow.Tours.Add(_mapper.Map<Tour>(tour));
            _uow.SaveChanges();
        }

        // PUT: api/Tour/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] TourVM tour)
        {
            var t = _uow.Tours.Get(id);
            t.Name = tour.Name;
            t.Image = tour.Image;
            t.Images = tour.Images;
            t.Description = tour.Description;
            t.DepartureDate = tour.DepartureDate;
            t.Slot = tour.Slot;
            t.Censorship = tour.Censorship;
            t.Status = tour.Status;
            t.Deleted = tour.Deleted;
            t.TourCategoryId = tour.TourCategoryId;
            _uow.Tours.Update(t);
            var result = _uow.SaveChanges();
            
        }

        // DELETE: api/Tour/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.Tours.Remove(_uow.Tours.Get(id));
            _uow.SaveChanges();
        }
    }
}
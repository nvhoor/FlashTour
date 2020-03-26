using System;
using System.Collections.Generic;
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
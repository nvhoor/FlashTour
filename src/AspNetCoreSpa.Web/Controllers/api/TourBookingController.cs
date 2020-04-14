using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class TourBookingController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public TourBookingController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/TourBookings
        [HttpGet]
        public IActionResult Get()
        {
            var allTourTourBookings = _uow.TourBookings.GetAll();
            return Ok(_mapper.Map<IEnumerable<TourBookingVM>>(allTourTourBookings));
        }

        // GET: api/TourBookings/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var booking = _uow.TourBookings.Get(id);
            return Ok(_mapper.Map<TourBookingVM>(booking));
        }

        // POST: api/TourBooking
        [HttpPost]
        public void Post([FromBody] TourBookingVM tourBooking)
        {
            tourBooking.BookingPrices.Clear();
            tourBooking.Id = Guid.NewGuid();
            for (var i=0;i<3;i++ )
            {
                var tourBookingPrice = new BookingPriceVM();
                tourBookingPrice.Id = Guid.NewGuid();
                tourBookingPrice.TourBookingId = tourBooking.Id;
                tourBookingPrice.TouristType = i;
                var prices = _uow.Prices.GetSingleOrDefault(x => x.TouristType == i && x.TourId == tourBooking.TourId);
                var toCheck = DateTime.UtcNow;
                var min = prices.StartDatePro;
                var max = prices.EndDatePro;
                if (DateTime.Compare(toCheck,min)>=0 && DateTime.Compare(toCheck, max)<=0)
                {
                    tourBookingPrice.Price = prices.PromotionPrice;
                }
                else
                {
                    tourBookingPrice.Price = prices.OriginalPrice;
                }
                tourBooking.BookingPrices.Add(tourBookingPrice);
            }
            foreach (var tourBookingTourCustomer in tourBooking.TourCustomers)
            {
                tourBookingTourCustomer.Id = Guid.NewGuid();
                tourBookingTourCustomer.TourBookingId = tourBooking.Id;
            }
            _uow.TourBookings.Add(_mapper.Map<TourBooking>(tourBooking));
            _uow.SaveChanges();
        }

        // PUT: api/TourBooking/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] TourBookingVM tbooking)
        {
            var booking = _uow.TourBookings.Get(id);
            booking.FullName = tbooking.FullName;
            booking.Email = tbooking.Email;
            booking.Mobile = tbooking.Mobile;
            booking.Note = tbooking.Note;
            booking.Address = tbooking.Address;
            booking.UserId = tbooking.UserId;
            booking.Status = tbooking.Status;
            booking.Deleted = tbooking.Deleted;
            _uow.TourBookings.Update(booking);
            var result = _uow.SaveChanges();
        }

        // DELETE: api/TourBookings/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.TourBookings.Remove(_uow.TourBookings.Get(id));
            _uow.SaveChanges();
        }
    }
}
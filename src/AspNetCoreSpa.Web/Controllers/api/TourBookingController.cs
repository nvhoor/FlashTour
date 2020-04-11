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
                    tourBooking.Id=Guid.NewGuid();
                    foreach (var tourBookingBookingPrice in tourBooking.BookingPrices)
                    {tourBookingBookingPrice.Id=Guid.NewGuid();
                        tourBookingBookingPrice.TourBookingId = tourBooking.Id;
                    }
                    foreach (var tourBookingTourCustomer in tourBooking.TourCustomers)
                    {
                        tourBookingTourCustomer.Id=Guid.NewGuid();
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
using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreSpa.Core;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class TourCustomerController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public TourCustomerController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/TourCustomer
        [HttpGet]
        public IActionResult Get()
        {
            var allCustomer = _uow.TourCustomers.GetAll();
            return Ok(_mapper.Map<IEnumerable<TourCustomerVM>>(allCustomer));
        }

        // GET: api/TourCustomer/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var customer = _uow.TourCustomers.Get(id);
            return Ok(_mapper.Map<TourCustomerVM>(customer));
        }

        // POST: api/TourCustomer
        [HttpPost]
        public void Post([FromBody] TourCustomerVM tourcust)
        {
            tourcust.Id=Guid.NewGuid();
            _uow.TourCustomers.Add(_mapper.Map<TourCustomer>(tourcust));
            _uow.SaveChanges();
        }
        [HttpPost("Array")]
        public void Post([FromBody] TourCustomerVM[] tourcusts)
        {
            foreach (var tourCustomerVm in tourcusts)
            {
                _uow.TourCustomers.Add(_mapper.Map<TourCustomer>(tourCustomerVm));
                _uow.SaveChanges();
            }
            var numberSlots = tourcusts.Length;
            var tourBookingId = tourcusts[0].TourBookingId;
            var tourBooking = _uow.TourBookings.Find(x => x.Id == tourBookingId).SingleOrDefault();
            _uow.Tours.DecreaseSlotsLeft(tourBooking.TourId,numberSlots);
        }
        // PUT: api/TourCustomer/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] TourCustomerVM customer)
        {
            var cust = _uow.TourCustomers.Get(id);
            cust.FullName = customer.FullName;
            cust.Gender = customer.Gender;
            cust.BirthDay = customer.BirthDay;
            cust.TourBookingId = customer.TourBookingId;
            cust.TouristType = customer.TouristType;
            _uow.TourCustomers.Update(cust);
            var result = _uow.SaveChanges();
            
        }

        // DELETE: api/TourCustomer/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.TourCustomers.Remove(_uow.TourCustomers.Get(id));
            _uow.SaveChanges();
        }
        // Danh sach khach hang di tour - tim theo ID Tour
        // GET: api/toucustomer/tourcustomerbytour
        [HttpGet("cusbytour/{id}")]
        public IActionResult GetTourCustomer(Guid id)
        {
            var t = _uow.Tours.Get(id); 
            var allcustomer =
                from tourcus in _uow.TourCustomers
                join tourBooking in _uow.TourBookings on tourcus.TourBookingId equals tourBooking.Id
                where t.Id == tourBooking.TourId
                select new
                    TourCustomerVM()
                    {
                        Id = tourcus.Id,
                        FullName = tourcus.FullName,
                        Gender = tourcus.Gender,
                        BirthDay = tourcus.BirthDay,
                        TourBookingId = tourcus.TourBookingId,
                        TouristType = tourcus.TouristType,
                        TourId = tourBooking.TourId
                    };
            return Ok(allcustomer);
        }
    }
}
using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
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
            _uow.TourCustomers.Add(_mapper.Map<TourCustomer>(tourcust));
            _uow.SaveChanges();
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
            cust.TouristTypeId = customer.TouristTypeId;
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
    }
}
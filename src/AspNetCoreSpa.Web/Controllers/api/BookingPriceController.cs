using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreSpa.Web.Controllers.api
{
    public class BookingPriceController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public BookingPriceController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/BookingPrice
        [HttpGet]
        public IActionResult Get()
        {
            var allBookingPrices = _uow.BookingPrices.GetAll();
            return Ok(_mapper.Map<IEnumerable<BookingPriceVM>>(allBookingPrices));
        }

        // GET: api/BookingPrice/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var bookingPrice = _uow.BookingPrices.Get(id);
            return Ok(_mapper.Map<TourCategoryVM>(bookingPrice));
        }

        // POST: api/BookingPrice
        [HttpPost]
        public void Post([FromBody] BookingPriceVM bookingPrice)
        {
            _uow.BookingPrices.Add(_mapper.Map<BookingPrice>(bookingPrice));
            _uow.SaveChanges();
        }
        // POST: api/BookingPrice/Array
        [HttpPost("Array")]
        public void Post([FromBody] BookingPriceVM[] bookingPrices)
        {
            foreach (var bookingPriceVm in bookingPrices)
            {
                _uow.BookingPrices.Add(_mapper.Map<BookingPrice>(bookingPriceVm));
                _uow.SaveChanges();
            }
        }
        // PUT: api/BookingPrice/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] BookingPriceVM bookingPrice)
        {
            var bookingPriceE= _uow.BookingPrices.Get(id);
            bookingPriceE.Price = bookingPrice.Price;
            _uow.BookingPrices.Update(bookingPriceE);
            var result = _uow.SaveChanges();
        }

        // DELETE: api/BookingPrice/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.BookingPrices.Remove(_uow.BookingPrices.Get(id));
            _uow.SaveChanges();
        }
    }
}
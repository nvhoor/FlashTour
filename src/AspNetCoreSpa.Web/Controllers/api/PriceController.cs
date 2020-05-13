﻿using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class PriceController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public PriceController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/Price
        [HttpGet]
        public IActionResult Get()
        {
            var allPrice = _uow.Prices.GetAll();
            return Ok(_mapper.Map<IEnumerable<PriceVM>>(allPrice));
        }

        // GET: api/Price/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var price = _uow.Prices.Get(id);
            return Ok(_mapper.Map<PriceVM>(price));
        }
        // GET Price by TourID
        [HttpGet("ByTourId")]
        public IActionResult Get(Guid tourId,int touristType)
        {
            var customer = _uow.Prices.GetAll().Where(x=>x.TourId==tourId
                                                                &&x.TouristType==touristType);
            return Ok(_mapper.Map<IEnumerable<PriceVM>>(customer));
        }
        // POST: api/Price
        [HttpPost]
        public void Post([FromBody] PriceVM price)
        {
            _uow.Prices.Add(_mapper.Map<Price>(price));
            _uow.SaveChanges();
        }

        // PUT: api/Price/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] PriceVM price)
        {
            var pr = _uow.Prices.Get(id);
            pr.Name = price.Name;
            pr.OriginalPrice = price.OriginalPrice;
            pr.PromotionPrice = price.PromotionPrice;
            pr.StartDatePro = price.StartDatePro;
            pr.TouristType = price.TouristType;
            _uow.Prices.Update(pr);
            var result = _uow.SaveChanges();
            
        }

        // DELETE: api/Price/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.Prices.Remove(_uow.Prices.Get(id));
            _uow.SaveChanges();
        }
    }
}
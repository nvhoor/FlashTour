using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class ProvinceController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public ProvinceController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/Province
        [HttpGet]
        public IActionResult Get()
        {
            var allProvince = _uow.Provinces.GetAll().OrderBy(x=>x.Name);
            return Ok(_mapper.Map<IEnumerable<ProvinceVM>>(allProvince));
        }

        // GET: api/Province/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var province = _uow.Provinces.Get(id);
            return Ok(_mapper.Map<ProvinceVM>(province));
        }

        // POST: api/Province
        [HttpPost]
        public void Post([FromBody] ProvinceVM province)
        {
            _uow.Provinces.Add(_mapper.Map<Province>(province));
            _uow.SaveChanges();
        }

    }
}
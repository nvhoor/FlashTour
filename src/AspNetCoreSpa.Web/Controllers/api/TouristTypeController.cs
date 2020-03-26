using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class TouristTypeController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public TouristTypeController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/TouristType
        [HttpGet]
        public IActionResult Get()
        {
            var allTourist = _uow.TouristTypes.GetAll();
            return Ok(_mapper.Map<IEnumerable<TourVM>>(allTourist));
        }

        // GET: api/TouristType/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var tourist = _uow.TouristTypes.Get(id);
            return Ok(_mapper.Map<TouristTypeVM>(tourist));
        }

        // POST: api/TouristType
        [HttpPost]
        public void Post([FromBody] TouristTypeVM tourist)
        {
            _uow.TouristTypes.Add(_mapper.Map<TouristType>(tourist));
            _uow.SaveChanges();
        }

        // PUT: api/TouristType/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] TouristTypeVM tourist)
        {
            var t = _uow.TouristTypes.Get(id);
            t.Name = tourist.Name;
            _uow.TouristTypes.Update(t);
            var result = _uow.SaveChanges();
            
        }

        // DELETE: api/TouristType/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.TouristTypes.Remove(_uow.TouristTypes.Get(id));
            _uow.SaveChanges();
        }
    }
}
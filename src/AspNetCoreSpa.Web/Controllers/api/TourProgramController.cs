using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class TourProgramController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public TourProgramController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/TourProgram
        [HttpGet]
        public IActionResult Get()
        {
            var allTourProgram = _uow.TourPrograms.GetAll();
            return Ok(_mapper.Map<IEnumerable<TourVM>>(allTourProgram));
        }

        // GET: api/TourProgram/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var tourprogram = _uow.TourPrograms.Get(id);
            return Ok(_mapper.Map<TourProgramVM>(tourprogram));
        }

        // POST: api/TourProgram
        [HttpPost]
        public void Post([FromBody] TourProgramVM tp)
        {
            _uow.TourPrograms.Add(_mapper.Map<TourProgram>(tp));
            _uow.SaveChanges();
        }

        // PUT: api/TourProgram/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] TourProgramVM tourProgram)
        {
            var tpro = _uow.TourPrograms.Get(id);
            tpro.Date = tourProgram.Date;
            tpro.OrderNumber = tourProgram.OrderNumber;
            tpro.Title = tourProgram.Title;
            tpro.Description = tourProgram.Description;
            tpro.Destination = tourProgram.Destination;
            tpro.TourId = tourProgram.TourId;
            _uow.TourPrograms.Update(tpro);
            var result = _uow.SaveChanges();
            
        }

        // DELETE: api/TourProgram/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.TourPrograms.Remove(_uow.TourPrograms.Get(id));
            _uow.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class EvaluationController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public EvaluationController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/Evaluations
        [HttpGet]
        public IActionResult Get()
        {
            var allEvaluations = _uow.Evaluations.GetAll();
            return Ok(_mapper.Map<IEnumerable<EvaluationVM>>(allEvaluations));
        }

        // GET: api/Evaluations/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var evaluation = _uow.Evaluations.Get(id);
            return Ok(_mapper.Map<EvaluationVM>(evaluation));
        }
        [HttpGet("GetEvaluationByTourId/{tourId}")]
        public IActionResult GetEvaluationByTourId(Guid tourId)
        {
            var evaluation = _uow.Evaluations.GetSingleOrDefault(x=>x.TourId==tourId);
            return Ok(_mapper.Map<EvaluationVM>(evaluation));
        }

        // POST: api/Evaluations
        [HttpPost]
        public void Post([FromBody] EvaluationVM evaluation)
        {
            _uow.Evaluations.Add(_mapper.Map<Evaluation>(evaluation));
            _uow.SaveChanges();
        }

        // PUT: api/Evaluations/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] int rate)
        {
            var e = _uow.Evaluations.Get(id);
            if (e != null)
            {
                switch (rate)
                {
                    case 1:
                        e.OneStar += 1;
                        break;
                    case 2:
                        e.TwoStar += 1;
                        break;
                    case 3:
                        e.ThreeStar += 1;
                        break;
                    case 4:
                        e.FourStar += 1;
                        break;
                    case 5:
                        e.FiveStar += 1;
                        break;
                }
                _uow.Evaluations.Update(e);
                var result = _uow.SaveChanges();
            }
           
        }

        // DELETE: api/Evaluations/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.Evaluations.Remove(_uow.Evaluations.Get(id));
            _uow.SaveChanges();
        }
    }
}
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
        public IActionResult Get(int id)
        {
            var evaluation = _uow.Evaluations.Get(id);
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
        public void Put(int id, [FromBody] EvaluationVM ev)
        {
            var e = _uow.Evaluations.Get(id);
            e.OneStar = ev.OneStar;
            e.TwoStar = ev.TwoStar;
            e.ThreeStar = ev.ThreeStar;
            e.FourStar = ev.FourStar;
            e.FiveStar = ev.FiveStar;
            _uow.Evaluations.Update(e);
            var result = _uow.SaveChanges();
        }

        // DELETE: api/Evaluations/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _uow.Evaluations.Remove(_uow.Evaluations.Get(id));
            _uow.SaveChanges();
        }
    }
}
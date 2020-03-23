using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreSpa.Web.Controllers.api
{
    public class TourCategoryController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public TourCategoryController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/TourCategories
        [HttpGet]
        public IActionResult Get()
        {
            var allTourCategories = _uow.TourCategories.GetAll();
            return Ok(_mapper.Map<IEnumerable<TourCategoryVM>>(allTourCategories));
        }

        // GET: api/TourCategories/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _uow.TourCategories.Get(id);
            return Ok(_mapper.Map<TourCategoryVM>(category));
        }

        // POST: api/TourCategories
        [HttpPost]
        public void Post([FromBody] TourCategoryVM tourCategory)
        {
            _uow.TourCategories.Add(_mapper.Map<TourCategory>(tourCategory));
            _uow.SaveChanges();
        }

        // PUT: api/TourCategories/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TourCategoryVM category)
        {
            var cat = _uow.TourCategories.Get(id);
            cat.Name = category.Name;
            cat.Description = category.Description;
            cat.Image = category.Image;
            _uow.TourCategories.Update(cat);
            var result = _uow.SaveChanges();
        }

        // DELETE: api/TourCategories/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _uow.TourCategories.Remove(_uow.TourCategories.Get(id));
            _uow.SaveChanges();
        }
    }
}
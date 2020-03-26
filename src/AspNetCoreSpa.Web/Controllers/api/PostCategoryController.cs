using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class PostCategoryController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public PostCategoryController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/PostCategories
        [HttpGet]
        public IActionResult Get()
        {
            var allPostCategories = _uow.PostCategories.GetAll();
            return Ok(_mapper.Map<IEnumerable<PostCategoryVM>>(allPostCategories));
        }

        // GET: api/PostCategories/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var category = _uow.PostCategories.Get(id);
            return Ok(_mapper.Map<PostCategoryVM>(category));
        }

        // POST: api/PostCategories
        [HttpPost]
        public void Post([FromBody] PostCategoryVM postCategory)
        {
            _uow.PostCategories.Add(_mapper.Map<PostCategory>(postCategory));
            _uow.SaveChanges();
        }

        // PUT: api/PostCategories/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] PostCategory category)
        {
            var poca = _uow.PostCategories.Get(id);
            poca.Name = category.Name;
        }

        // DELETE: api/PostCategories/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.PostCategories.Remove(_uow.PostCategories.Get(id));
            _uow.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Get(Guid id)
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
        public void Put(Guid id, [FromBody] TourCategoryVM category)
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
        public void Delete(Guid id)
        {
            _uow.TourCategories.Remove(_uow.TourCategories.Get(id));
            _uow.SaveChanges();
        }
        // PUT: api/tour/uploadImage
        [HttpPost("UploadImage")]
        [DisableRequestSizeLimit]
        [Authorize(Roles = ("admin,Admin,staff,Staff"))]
        public IActionResult UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("ClientApp", "src","assets","images","tour-categories");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
 
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
 
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
 
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
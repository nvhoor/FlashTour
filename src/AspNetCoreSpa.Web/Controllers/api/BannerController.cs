using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class BannerController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public BannerController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/Banner
        [HttpGet]
        public IActionResult Get()
        {
            var allBanner = _uow.Banners.GetAll();
            return Ok(_mapper.Map<IEnumerable<BannerVM>>(allBanner));
        }

        // GET: api/Banner/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var banner = _uow.Banners.Get(id);
            return Ok(_mapper.Map<BannerVM>(banner));
        }

        // POST: api/Banner
        [HttpPost]
        public void Post([FromBody] BannerVM banner)
        {
            _uow.Banners.Add(_mapper.Map<Banner>(banner));
            _uow.SaveChanges();
        }

        // PUT: api/Banner/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] BannerVM banner)
        {
            var b = _uow.Banners.Get(id);
            b.Name = banner.Name;
            b.Image = banner.Image;
            b.Description = banner.Description;
            _uow.Banners.Update(b);
            var result = _uow.SaveChanges();
            
        }

        // DELETE: api/Banner/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _uow.Banners.Remove(_uow.Banners.Get(id));
            _uow.SaveChanges();
        }
    }
}
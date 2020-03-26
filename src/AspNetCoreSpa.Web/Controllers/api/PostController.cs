using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class PostController : BaseController
    {
        
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public PostController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/Post
        [HttpGet]
        public IActionResult Get()
        {
            var allPost = _uow.Posts.GetAll();
            return Ok(_mapper.Map<IEnumerable<PostVM>>(allPost));
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var post = _uow.Posts.Get(id);
            return Ok(_mapper.Map<PostVM>(post));
        }

        // POST: api/Post
        [HttpPost]
        public void Post([FromBody] PostVM post)
        {
            _uow.Posts.Add(_mapper.Map<Post>(post));
            _uow.SaveChanges();
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] PostVM post)
        {
            var p = _uow.Posts.Get(id);
            p.Name = post.Name;
            p.PostContent = post.PostContent;
            p.Description = post.Description;
            p.Image = post.Image;
            p.MetaDescription = post.MetaDescription;
            p.MetaKeyWord = post.MetaKeyWord;
            p.Alias = post.Alias;
            p.Status = post.Status;
            p.Censorship = post.Censorship;
            p.PostCategoryId = post.PostCategoryId;
            _uow.Posts.Update(p);
            var result = _uow.SaveChanges();
            
        }

        // DELETE: api/Tour/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.Posts.Remove(_uow.Posts.Get(id));
            _uow.SaveChanges();
        }
    }
}
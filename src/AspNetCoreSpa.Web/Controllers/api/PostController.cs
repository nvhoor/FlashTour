using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using System.Linq;
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
        //GET: api/post
        [HttpGet]
        public IActionResult GetAll()
        {
            var allPost =
                from post in _uow.Posts
                where post.Censorship && post.Deleted == false
                select new
                    PostVM()
                    {
                        Id = post.Id,
                        Name = post.Name,
                        PostContent = post.PostContent,
                        Description = post.Description,
                        Image = post.Image,
                        MetaDescription = post.MetaDescription,
                        MetaKeyWord = post.MetaKeyWord,
                        Alias = post.Alias,
                        Status = post.Status,
                        Censorship = post.Censorship,
                        PostCategoryId = post.PostCategoryId,
                    };
            return Ok(allPost);
        }

        // GET: api/post/{id}
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var postid = _uow.Posts.Get(id);
            return Ok(_mapper.Map<PostVM>(postid));
        }

        // POST: api/post
        [HttpPost]
        public void Post([FromBody] PostVM post)
        {
            _uow.Posts.Add(_mapper.Map<Post>(post));
            _uow.SaveChanges();
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Post post)
        {
            var pos = _uow.Posts.Get(id);
            pos.Name = post.Name;
        }

        // DELETE: api/post/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.Posts.Remove(_uow.Posts.Get(id));
            _uow.SaveChanges();
        }
        // Danh sach bai viet chua kiem duyet
        // GET: 
        [HttpGet("cencershippost")]
        public IActionResult GetStatusPost()
        {
            var allPost =
                from post in _uow.Posts
                where post.Censorship == false
                select new
                    PostVM()
                    {
                        Id = post.Id,
                        Name = post.Name,
                        PostContent = post.PostContent,
                        Description = post.Description,
                        Image = post.Image,
                        MetaDescription = post.MetaDescription,
                        MetaKeyWord = post.MetaKeyWord,
                        Alias = post.Alias,
                        Status = post.Status,
                        Censorship = post.Censorship,
                        PostCategoryId = post.PostCategoryId,
                    };
            return Ok(allPost);
        }
        // Danh sach bai viet theo PostCate
        [HttpGet("postsByCate/{id}")]
        public IActionResult GetPostsByCategory(Guid id)
        {
            var posts = _uow.Posts.Get(id);
            var postCategory = _uow.PostCategories.Get(posts.PostCategoryId);
            var allPost = (from post in _uow.Posts
                join postCate in _uow.PostCategories
                    on post.PostCategoryId equals postCate.Id
                where postCate.Id == postCategory.Id
                select new
                    PostVM()
                    {
                        Id = post.Id,
                        Name = post.Name,
                        PostContent = post.PostContent,
                        Description = post.Description,
                        Image = post.Image,
                        MetaDescription = post.MetaDescription,
                        MetaKeyWord = post.MetaKeyWord,
                        Alias = post.Alias,
                        Status = post.Status,
                        Censorship = post.Censorship,
                        PostCategoryId = post.PostCategoryId,
                    }).Take(8);
            return Ok(allPost);
        }
         
    }
}

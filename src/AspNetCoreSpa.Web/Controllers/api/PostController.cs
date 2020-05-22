using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
            var allPosts = _uow.Posts.GetAll().Where(x => x.Censorship && !x.Deleted && x.Status);
            var allPostsVm = _mapper.Map<IEnumerable<PostVM>>(allPosts);
            foreach (var postVm in allPostsVm)
            {
                var postCategory = _uow.PostCategories.GetSingleOrDefault(x => x.Id == postVm.PostCategoryId);
                if (postCategory!=null)
                {
                    postVm.CategoryName = postCategory.Name;
                }
            }
            return Ok(allPostsVm);
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

        // PUT: api/post/
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Post post)
        {
            var pos = _uow.Posts.Get(id);
            pos.Name = post.Name;
            pos.PostContent = post.PostContent;
            pos.Description = post.Description;
            pos.Image = post.Image;
            pos.MetaDescription = post.MetaDescription;
            pos.MetaKeyWord = post.MetaKeyWord;
            pos.Alias = post.Alias;
            pos.UpdatedAt = post.UpdatedAt;
            pos.CreatedAt = post.CreatedAt;
            pos.PostCategoryId = post.PostCategoryId;
            _uow.Posts.Update(pos);
            var result = _uow.SaveChanges();
        }

        // DELETE: api/post/
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
         // PUT: api/post/accepttour/{id}
                 [HttpPut("acceptpost/{id}")]
                 public void Putt(Guid id)
                 {
                     var p = _uow.Posts.Get(id);
                     p.Censorship = true;
                     _uow.Posts.Update(p);
                     var result = _uow.SaveChanges();
                 }
        [HttpGet("postsSameCate/{id}")]
        public IActionResult GetPostsSameCategory(Guid Id)
        {
            var postE = _uow.Posts.Get(Id);
            var postCategoryE = _uow.PostCategories.Get(postE.PostCategoryId);
            var allPost = (from post in _uow.Posts
                join postCate in _uow.PostCategories
                    on post.PostCategoryId equals postCate.Id
                where post.Deleted==false&&post.Status&&postCate.Id==postCategoryE.Id&&post.Id!=Id&&post.Censorship
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
                            
                    }).OrderByDescending(x=>x.CreatedAt).Take(4);
            return Ok(allPost);
        }
        
        [HttpGet("Search")]
        public IActionResult Get(string postCategoryId)
        {
            var conditionCate = postCategoryId != "0";
            var allTour = (from tour in _uow.Posts
                join tourCate in _uow.PostCategories
                    on tour.PostCategoryId equals tourCate.Id
                where (tour.Deleted == false && tour.Censorship && tour.Status
                       && (!conditionCate || tourCate.Id == Guid.Parse(postCategoryId)))
                select new
                    PostVM()
                    {
                        Id = tour.Id,
                        Description = tour.Description,
                        Image = tour.Image,
                        Name = tour.Name,
                    });
            return Ok(allTour);
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
                var folderName = Path.Combine("ClientApp", "src","assets","images","posts");
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
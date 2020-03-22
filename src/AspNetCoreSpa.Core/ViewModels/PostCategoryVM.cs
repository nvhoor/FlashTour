using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class PostCategoryVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }  
    }
}
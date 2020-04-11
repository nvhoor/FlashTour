using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class ContactVM
    {
        public Guid Id { get; set; }
        
        public string FullName { get; set; }
        [EmailAddress]
        public string Email {get; set;}
        
        public string Phone {get; set;}
        
        public string Address {get; set;}
        
        public string Title {get; set;}
        public string Content {get; set;}
        public string Information {get; set;}

    }
}
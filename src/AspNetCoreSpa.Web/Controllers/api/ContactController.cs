using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class ContactController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public ContactController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/Contact
        [HttpGet]
        public IActionResult Get()
        {
            var allContact = _uow.Contacts.GetAll().Where(x => x.Check == false);
            return Ok(_mapper.Map<IEnumerable<ContactVM>>(allContact));
        }

        // GET: api/Contact/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var contact = _uow.Contacts.Get(id);
            return Ok(_mapper.Map<ContactVM>(contact));
        }

        // POST: api/Contact
        [HttpPost]
        public void Post([FromBody] ContactVM contact)
        {
            _uow.Contacts.Add(_mapper.Map<Contact>(contact));
            _uow.SaveChanges();
        }

        // PUT: api/Contact/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] ContactVM contact)
        {
            var ct = _uow.Contacts.Get(id);
            ct.FullName = contact.FullName;
            ct.Email = contact.Email;
            ct.Phone = contact.Phone;
            ct.Address = contact.Address;
            ct.Title = contact.Title;
            ct.Content = contact.Content;
            ct.Information = contact.Information;
            ct.Check = true;
            ct.Note = contact.Note;
            _uow.Contacts.Update(ct);
            var result = _uow.SaveChanges();
        }

        // DELETE: api/Contact/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.Contacts.Remove(_uow.Contacts.Get(id));
            _uow.SaveChanges();
        }
        // Danh sach Contact da check
        // GET: 
        [HttpGet("censorship")]
        public IActionResult GetStatusContact()
        {
            var allContact =
                from contact in _uow.Contacts
                where contact.Check == true
                select new
                    ContactVM()
                    {
                        FullName = contact.FullName,
                        Email = contact.Email,
                        Phone = contact.Phone,
                        Address = contact.Address,
                        Title = contact.Title,
                        Content = contact.Content,
                        Information = contact.Information,
                        Note =  contact.Note,
                    };
            return Ok(allContact);
        }
    }
}
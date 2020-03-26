using System;
using System.Collections.Generic;
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
            var allContact = _uow.Contacts.GetAll();
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
    }
}
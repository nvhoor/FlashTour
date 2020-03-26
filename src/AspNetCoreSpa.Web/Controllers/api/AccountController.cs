using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreSpa.Web.Controllers.api
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public AccountController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/Account
        [HttpGet]
        public IActionResult Get()
        {
            var allAccount = _uow.Accounts.GetAll();
            return Ok(_mapper.Map<IEnumerable<AccountVM>>(allAccount));
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var account = _uow.Accounts.Get(id);
            return Ok(_mapper.Map<AccountVM>(account));
        }

        // POST: api/Account
        [HttpPost]
        public void Post([FromBody] AccountVM account)
        {
            _uow.Accounts.Add(_mapper.Map<Account>(account));
            _uow.SaveChanges();
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] AccountVM account)
        {
            var a = _uow.Accounts.Get(id);
            a.RoleId = account.RoleId;
            _uow.Accounts.Update(a);
            var result = _uow.SaveChanges();
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _uow.Accounts.Remove(_uow.Accounts.Get(id));
            _uow.SaveChanges();
        }
    }
}
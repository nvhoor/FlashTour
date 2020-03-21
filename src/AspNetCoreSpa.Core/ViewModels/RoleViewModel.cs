using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<AccountViewModel> Accounts { get; set; }
    }
}
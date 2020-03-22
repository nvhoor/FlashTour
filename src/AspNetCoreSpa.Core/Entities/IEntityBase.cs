using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSpa.Core.Entities
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}

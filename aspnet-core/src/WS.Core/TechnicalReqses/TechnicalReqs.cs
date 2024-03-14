using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace WS.TechnicalReqses
{
    [Table("TechnicalReqses")]
    public class TechnicalReqs : Entity<long>
    {

        [Required]
        public virtual string Value { get; set; }

    }
}
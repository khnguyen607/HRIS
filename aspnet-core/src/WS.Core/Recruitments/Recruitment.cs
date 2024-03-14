using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace WS.Recruitments
{
    [Table("Recruitments")]
    public class Recruitment : CreationAuditedEntity<long>
    {

        [Required]
        public virtual string Title { get; set; }

        public virtual string Jd { get; set; }

    }
}
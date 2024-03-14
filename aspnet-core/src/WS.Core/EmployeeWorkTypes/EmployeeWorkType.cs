using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace WS.EmployeeWorkTypes
{
    [Table("EmployeeWorkTypes")]
    public class EmployeeWorkType : Entity<long>
    {

        [Required]
        public virtual string Type { get; set; }

    }
}
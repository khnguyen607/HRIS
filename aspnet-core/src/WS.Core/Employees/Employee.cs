using WS.EmployeeWorkTypes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace WS.Employees
{
    [Table("Employees")]
    public class Employee : CreationAuditedEntity<long>
    {

        public virtual string FullName { get; set; }

        public virtual string Address { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Department { get; set; }

        public virtual string JobTitle { get; set; }

        public virtual string SocialSecurity { get; set; }

        public virtual long? EmployeeWorkTypeId { get; set; }

        [ForeignKey("EmployeeWorkTypeId")]
        public EmployeeWorkType EmployeeWorkTypeFk { get; set; }

    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace WS.Recruitments
{
    [Table("RecruitmentCandidates")]
    public class RecruitmentCandidate : Entity<long>
    {

        [Required]
        public virtual string FullName { get; set; }

        public virtual int? Age { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Email { get; set; }

        public virtual string Cv { get; set; }

        public virtual int Point { get; set; }

        public virtual long RecruitmentId { get; set; }

    }
}
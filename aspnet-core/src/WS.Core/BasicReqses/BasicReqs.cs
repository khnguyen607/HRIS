﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace WS.BasicReqses
{
    [Table("BasicReqses")]
    public class BasicReqs : Entity<long>
    {

        public virtual string Value { get; set; }

    }
}
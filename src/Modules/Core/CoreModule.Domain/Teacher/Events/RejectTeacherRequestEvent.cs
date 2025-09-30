﻿using Common.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Teacher.Events;

public class RejectTeacherRequestEvent : BaseDomainEvent
{
    public string Description { get; set; }
    public Guid UserId { get; set; }
}
public class AcceptTeacherRequestEvent : BaseDomainEvent
{
    public Guid UserId { get; set; }
}

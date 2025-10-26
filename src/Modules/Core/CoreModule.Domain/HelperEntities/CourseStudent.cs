using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.HelperEntities;

public class CourseStudent : BaseEntity
{
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
}

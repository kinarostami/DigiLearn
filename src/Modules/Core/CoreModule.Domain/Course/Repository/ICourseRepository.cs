using Common.Domain.Repository;
using CoreModule.Domain.Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Course.Repository
{
    public interface ICourseRepository : IBaseRepository<Models.Course>
    {
    }
}

using Common.Domain.Repository;
using CoreModule.Domain.Category.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Category.Repository
{
    public interface ICourseCategoryRepository : IBaseRepository<CourseCategory>
    {
        Task Delete(CourseCategory category);
    }
}

using Common.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.HelperEntities.Repositories;

public interface ICourseStudentRepository : IBaseRepository<CourseStudent>
{
    Task<CourseStudent?> GetCourseStudent(Guid courseId, Guid userId);
    void Delete(CourseStudent student);
}

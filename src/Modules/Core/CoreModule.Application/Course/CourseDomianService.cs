using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Course.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Course;

public class CourseDomianService : ICourseDomainService
{
    private readonly ICourseRepository _courseRepository;

    public CourseDomianService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public bool SlugIsExist(string slug)
    {
        return _courseRepository.Exists(x => x.Slug == slug.ToString());
    }
}

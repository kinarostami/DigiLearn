using Common.Infrastructure.Repository;
using CoreModule.Domain.Course.Repository;

namespace CoreModule.Infrastucture.Persistent.Course;

public class CourseRepository : BaseRepository<Domain.Course.Models.Course, CoreMoudelEfContext>, ICourseRepository
{
    public CourseRepository(CoreMoudelEfContext context) : base(context)
    {
    }

}

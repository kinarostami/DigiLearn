using CoreModule.Domain.Course.Repository;
using CoreModule.Domain.Order.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Order;

public class OrderDomainService : IOrderDomainService
{
    private readonly ICourseRepository _courseRepository;

    public OrderDomainService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<int> GetCoursePriceById(Guid courseId)
    {
        var course = await _courseRepository.GetAsync(courseId);
        if(course == null)
            return 0;

        return course.Price;
    }
}

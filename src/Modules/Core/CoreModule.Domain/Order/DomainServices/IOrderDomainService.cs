using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Order.DomainServices;

public interface IOrderDomainService
{
    Task<int> GetCoursePriceById(Guid courseId);
}

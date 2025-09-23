using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Category.DomainServices;

public interface ICategoryDomainService
{
    bool SlugIsExist(string slug);
}

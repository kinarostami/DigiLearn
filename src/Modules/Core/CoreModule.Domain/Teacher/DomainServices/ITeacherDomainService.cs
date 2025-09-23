using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Teacher.DomainServices
{
    public interface ITeacherDomainService
    {
        bool UserNameIsExists(string userName);
    }
}

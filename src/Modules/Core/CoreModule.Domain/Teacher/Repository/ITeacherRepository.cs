using Common.Domain.Repository;
using CoreModule.Domain.Teacher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Teacher.Repository
{
    public interface ITeacherRepository : IBaseRepository<Models.Teacher>
    {
        void Delete(Models.Teacher teacher);
    }
}

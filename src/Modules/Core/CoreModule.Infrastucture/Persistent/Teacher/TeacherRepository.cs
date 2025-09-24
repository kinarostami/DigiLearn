using Common.Infrastructure.Repository;
using CoreModule.Domain.Teacher.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Infrastucture.Persistent.Teacher;

public class TeacherRepository : BaseRepository<Domain.Teacher.Models.Teacher, CoreMoudelEfContext>, ITeacherRepository
{
    public TeacherRepository(CoreMoudelEfContext context) : base(context)
    {
    }

    public void Delete(Domain.Teacher.Models.Teacher teacher)
    {
        Context.Remove(teacher);
    }
}

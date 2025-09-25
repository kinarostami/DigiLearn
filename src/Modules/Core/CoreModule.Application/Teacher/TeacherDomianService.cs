using CoreModule.Domain.Teacher.DomainServices;
using CoreModule.Domain.Teacher.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Teacher
{
    public class TeacherDomianService : ITeacherDomainService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherDomianService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public bool UserNameIsExists(string userName)
        {
            return _teacherRepository.Exists(x => x.UserName == userName.ToLower());
        }
    }
}

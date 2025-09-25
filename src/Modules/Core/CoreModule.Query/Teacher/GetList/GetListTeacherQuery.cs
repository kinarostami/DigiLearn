using Common.Query;
using CoreModule.Domain.Teacher.Models;
using CoreModule.Query._Data;
using CoreModule.Query.Teacher._DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Teacher.GetList;

public class GetListTeacherQuery : IQuery<List<TeacherDto>>
{
}
public class GetListTeacherQueryHandler : IQueryHandler<GetListTeacherQuery, List<TeacherDto>>
{
    private readonly QueryContext _context;

    public GetListTeacherQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<TeacherDto>> Handle(GetListTeacherQuery request, CancellationToken cancellationToken)
    {
        return await _context.Teachers
            .Include(x => x.User)
            .Select(teacher => new TeacherDto()
            {
                Id = teacher.Id,
                CreationDate = teacher.CreationDate,
                CvFileName = teacher.CvFileName,
                Status = teacher.Status,
                UserName = teacher.UserName,
                User = new Query._DTOs.CoreModuleUserDto()
                {
                    Id = teacher.User.Id,
                    Avatar = teacher.User.Avatar,
                    CreationDate = teacher.User.CreationDate,
                    Email = teacher.User.Email,
                    PhoneNumber = teacher.User.PhoneNumber,
                    Name = teacher.User.Name,
                    Family = teacher.User.Family,
                }   
            }).ToListAsync(cancellationToken);
    }
}

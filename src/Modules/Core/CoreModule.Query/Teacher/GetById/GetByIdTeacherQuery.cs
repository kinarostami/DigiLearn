using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Teacher._DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Teacher.GetById;

public record GetByIdTeacherQuery(Guid Id) : IQuery<TeacherDto?>;

public class GetByIdTeacherQueryHandler : IQueryHandler<GetByIdTeacherQuery, TeacherDto?>
{
    private readonly QueryContext _context;

    public GetByIdTeacherQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<TeacherDto> Handle(GetByIdTeacherQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        if (teacher == null)
            return null;

        return new TeacherDto()
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
        };
    }
}

using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using CoreModule.Domain.Teacher.DomainServices;
using CoreModule.Domain.Teacher.Enums;
using CoreModule.Domain.Teacher.Events;

namespace CoreModule.Domain.Teacher.Models;

public class Teacher : AggregateRoot
{
    private Teacher()
    {
        
    }
    public Teacher(Guid userId, string userName, string cvFileName,ITeacherDomainService teacherDomainService)
    {
        NullOrEmptyDomainDataException.CheckString(userName, nameof(userName));
        NullOrEmptyDomainDataException.CheckString(cvFileName, nameof(cvFileName));
        if (userName.IsUniCode())
            throw new InvalidDomainDataException("UserName Invalid");

        if (teacherDomainService.UserNameIsExists(userName))
            throw new InvalidDomainDataException("UserName is exist");

        UserId = userId;
        UserName = userName.ToLower();
        CvFileName = cvFileName;
        Status = TeacherStatus.Pending;
    }

    public Guid UserId { get; private set; }
    public string UserName { get;private set; }
    public string CvFileName { get;private set; }
    public TeacherStatus Status { get; private set; }

    public void ToggleStatus()
    {
        if (Status == TeacherStatus.Active)
        {
            Status = TeacherStatus.Inactive;
        }
        else if (Status == TeacherStatus.Inactive)
        {
            Status = TeacherStatus.Active;
        }
    }

    public void AcceptRequest()
    {
        if (Status == TeacherStatus.Pending)
        {
            AddDomainEvent(new AcceptTeacherRequestEvent()
            {
                UserId = UserId
            });
            Status = TeacherStatus.Active;
        }
    }
}

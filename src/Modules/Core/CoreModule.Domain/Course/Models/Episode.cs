using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;

namespace CoreModule.Domain.Course.Models;

public class Episode : BaseEntity
{
    public Episode(string title, Guid token, TimeSpan timeSpan, string videoName, string? attachmentName, bool isActive, Guid sectionId, string englishTitle, bool isFree)
    {
        Guard(title, videoName, englishTitle);
        Title = title;
        Token = token;
        TimeSpan = timeSpan;
        VideoName = videoName;
        AttachmentName = attachmentName;
        IsActive = isActive;
        SectionId = sectionId;
        EnglishTitle = englishTitle;
        IsFree = isFree;
    }
    public Guid SectionId { get; private set; }
    public string Title { get; private set; }
    public string EnglishTitle { get; set; }
    public Guid Token { get; private set; }
    public TimeSpan TimeSpan { get; private set; }
    public string VideoName { get; private set; }
    public string? AttachmentName { get; private set; }
    public bool IsActive { get; set; }
    public bool IsFree { get; private set; }

    public void ToggleStatus()
    {
        IsActive = !IsActive;
    }

    internal void Edit(string title, bool isActive, TimeSpan timeSpan,string? attachmentName)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        IsActive = isActive;
        TimeSpan = timeSpan;
        if (string.IsNullOrWhiteSpace(attachmentName) == false)
        {
            AttachmentName = attachmentName;
        }
    }

    void Guard(string title,string videoName,string englishTitle)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(videoName, nameof(videoName));
        NullOrEmptyDomainDataException.CheckString(englishTitle, nameof(englishTitle));
        if (englishTitle.IsUniCode())
        {
            throw new InvalidDomainDataException("Invalid EnglishTitle");
        }
    }
}

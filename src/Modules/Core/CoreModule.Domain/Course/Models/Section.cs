using Common.Domain;
using Common.Domain.Exceptions;
using static System.Collections.Specialized.BitVector32;

namespace CoreModule.Domain.Course.Models;

public class Section : BaseEntity
{
    public Section(string title, int displayOrder, Guid courseId)
    {
        Title = title;
        DisplayOrder = displayOrder;
        CourseId = courseId;
        Episodes = new List<Episode>();
    }
    public Guid CourseId { get; private set; }
    public string Title { get;private set; }
    public int DisplayOrder { get;private set; }

    public IEnumerable<Episode> Episodes { get; private set; }

    public void Edit(string title,int disblayOrder)
    {
        Title = title;
        DisplayOrder = disblayOrder;
    }

    public void AddEpisode(string title, Guid token, TimeSpan timeSpan, string videoName, string? attachmentName, bool isActive,string englishTitle)
    {
       Episodes = Episodes.Append(new Episode(title, token, timeSpan, videoName, attachmentName, isActive, Id,englishTitle));
    }
}

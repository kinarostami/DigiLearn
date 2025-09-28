using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Category.DomainServices;
using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Course.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CoreModule.Domain.Course.Models;

public class Course : AggregateRoot
{
    private Course()
    {
        
    }
    public Course(Guid teacherId, string title, string description, string imageName, string? videoName,
        int price, SeoData seoData, CourseLevel courseLevel, Guid categoryId,
        Guid subCategoryId, string slug, ICourseDomainService courseDomainService, CourseActionStatus status)
    {
        Guard(title, description, imageName, slug);

        if (courseDomainService.SlugIsExist(slug))
            throw new InvalidDomainDataException("Slug is Exist");

        TeacherId = teacherId;
        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        Price = price;
        LastUpdate = DateTime.Now;
        SeoData = seoData;
        CourseLevel = courseLevel;
        CourseStatus = CourseStatus.StartSoon;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        Slug = slug;
        Status = status;
        Sections = new();
    }

    public Guid TeacherId { get;private set; }
    public Guid CategoryId { get;private set; }
    public Guid SubCategoryId { get;private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string ImageName { get; private set; }
    public string Slug { get; set; }
    public string? VideoName { get; private set; }
    public int Price { get; private set; }
    public DateTime LastUpdate { get; private set; }
    public SeoData SeoData { get; private set; }
    public CourseLevel CourseLevel { get; private set; }
    public CourseStatus CourseStatus { get; private set; }
    public CourseActionStatus Status { get;  set; }

    public List<Section> Sections { get; private set; }

    public void Edit(string title, string description, string imageName, string? videoName,
        int price, SeoData seoData, CourseLevel courseLevel,CourseStatus courseStatus, Guid categoryId,
        Guid subCategoryId, string slug,CourseActionStatus status, ICourseDomainService courseDomainService)
    {
        Guard(title, description, imageName, slug);
        if(slug != Slug)
            if (courseDomainService.SlugIsExist(slug))
                throw new InvalidDomainDataException("Slug is Exist");

        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        Price = price;
        LastUpdate = DateTime.Now;
        SeoData = seoData;
        CourseLevel = courseLevel;
        CourseStatus = courseStatus;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        Slug = slug;
        Status = status;
    }


    public void AddSection(string title,int displayOrder)
    {
        if (Sections.Any(x => x.Title == title))
            throw new InvalidDomainDataException("title is exist");

        Sections.Add(new Section(title,displayOrder,Id));
    }

    public void EditSection(Guid sectionId,string title,int displayOrder)
    {
        var section = Sections.FirstOrDefault(x => x.Id == sectionId);
        if (section == null)
            throw new InvalidDomainDataException("not remove sectoin");

       section.Edit(title,displayOrder);
    }

    public void RemoveSection(Guid sectionId)
    {
        var section = Sections.FirstOrDefault(x => x.Id == sectionId);
        if (section == null)
            throw new InvalidDomainDataException("not remove sectoin");

        Sections.Remove(section);
    }

    public void AddEpisode(Guid sectionId,string title, Guid token, TimeSpan timeSpan, string videoExtension, string? attachmentExtension, bool isActive,string engilshTitle,bool isFree)
    {
        
        var section = Sections.FirstOrDefault(x => x.Id == sectionId);
        if (section == null)
            throw new InvalidDomainDataException("not remove sectoin");

        var episodeCount = Sections.Sum(x => x.Episodes.Count());
        var episodeTitle = $"{episodeCount + 1}_{engilshTitle}";

        string attName = null;

        if (string.IsNullOrWhiteSpace(attachmentExtension) == false)
            attName = $"{episodeTitle}.{attachmentExtension}";
        var vidName = $"{ episodeTitle}.{ videoExtension }" ;

        if (isActive)
        {
            LastUpdate = DateTime.Now;
            if (CourseStatus == CourseStatus.StartSoon)
            {
                CourseStatus = CourseStatus.InProgress;
            }
        }
        section.AddEpisode(title, token,timeSpan,vidName,attName,isActive,engilshTitle,isFree);
    }

    public void AcceptEpisode(Guid episodeId)
    {
        var section = Sections.FirstOrDefault(x => x.Episodes.Any(f => f.Id == episodeId && f.IsActive == false));
        if (section == null)
            throw new InvalidDomainDataException();

        var episode = section.Episodes.First();

        episode.ToggleStatus();
        LastUpdate = DateTime.Now;
    }

    void Guard(string title,string description,string imageName,string slug)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
    }
}

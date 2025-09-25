using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using CoreModule.Domain.Category.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Category.Models;

public class CourseCategory : AggregateRoot
{
    private CourseCategory()
    {
        
    }
    public CourseCategory(string title, string slug, Guid? parentId, ICategoryDomainService categoryDomainService)
    {
        Guard(title, slug);
        if (categoryDomainService.SlugIsExist(slug))
            throw new InvalidDomainDataException("Slug is Exist");

        Title = title;
        Slug = slug;
        ParentId = parentId;
    }

    public string Title { get; set; }
    public string Slug { get; set; }
    public Guid? ParentId { get; set; }

    public void Edit(string title, string slug, ICategoryDomainService categoryDomainService)
    {
        Guard(title, slug);

        if (slug != Slug)
            if (categoryDomainService.SlugIsExist(slug))
                throw new InvalidDomainDataException("Slug is Exist");

        Title = title;
        Slug = slug;
    }

    void Guard(string title,string slug)
    {
        NullOrEmptyDomainDataException.CheckString(title,nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug,nameof(slug));

        if (slug.IsUniCode())
            throw new InvalidDomainDataException("Slug InValid");
    }
}

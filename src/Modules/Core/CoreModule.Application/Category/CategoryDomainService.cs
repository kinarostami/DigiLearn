using CoreModule.Domain.Category.DomainServices;
using CoreModule.Domain.Category.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Category;

public class CategoryDomainService : ICategoryDomainService
{
    private readonly ICourseCategoryRepository _courseCategoryRepository;

    public CategoryDomainService(ICourseCategoryRepository courseCategoryRepository)
    {
        _courseCategoryRepository = courseCategoryRepository;
    }

    public bool SlugIsExist(string slug)
    {
        return _courseCategoryRepository.Exists(x => x.Slug == slug.ToString());
    }
}

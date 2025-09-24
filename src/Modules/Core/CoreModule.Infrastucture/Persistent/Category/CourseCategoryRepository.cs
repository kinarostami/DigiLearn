using Common.Infrastructure.Repository;
using CoreModule.Domain.Category.Models;
using CoreModule.Domain.Category.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Infrastucture.Persistent.Category;

public class CourseCategoryRepository : BaseRepository<CourseCategory, CoreMoudelEfContext>, ICourseCategoryRepository
{
    public CourseCategoryRepository(CoreMoudelEfContext context) : base(context)
    {
    }

    public async Task Delete(CourseCategory category)
    {
        var categoryHasCourse = await Context.Courses
            .AnyAsync(x => x.Id == category.Id || x.SubCategoryId == category.Id);

        if (categoryHasCourse)
        {
            throw new Exception("این دسته بندی دارای چندیدن دوره است");
        }
        //todod shoulde remove childs
        Context.Remove(category);
        await Context.SaveChangesAsync();
    }
}

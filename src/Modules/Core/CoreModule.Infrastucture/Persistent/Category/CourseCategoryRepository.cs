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

        var children = await Context.Categories.Where(x => x.ParentId == category.Id).ToListAsync();
        if (children.Any())
        {
            foreach (var child in children)
            {
                var isAnyCourse = await Context.Courses
                .AnyAsync(x => x.Id == category.Id || x.SubCategoryId == category.Id);
                if (isAnyCourse)
                {
                    throw new Exception("این دسته بندی دارای چندیدن دوره است");
                }
                else
                {
                    Context.Remove(child);
                }
            }
        }

        Context.Remove(category);
        await Context.SaveChangesAsync();
        
    }
}

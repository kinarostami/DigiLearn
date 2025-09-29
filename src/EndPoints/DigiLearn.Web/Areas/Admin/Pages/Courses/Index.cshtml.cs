using Common.Query.Filter;
using CoreModue.Facade.Course;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses
{
    [BindProperties]
    public class IndexModel : BaseRazorFilter<CourseFilterParams>
    {
        private readonly ICourseFacade _courseFacade;

        public IndexModel(ICourseFacade courseFacade)
        {
            _courseFacade = courseFacade;
        }

        public CourseFilterResult FilterResult { get; set; }
        public async Task OnGet()
        {
            FilterResult = await _courseFacade.GetCourseFilter(FilterParams);
        }
    }
}

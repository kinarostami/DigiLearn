using DigiLearn.Web.Infrastructure.Services;
using DigiLearn.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IHomePageService _homePageService;

        public IndexModel(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        public HomePageViewModel HomePageData { get; set; }

        public async Task OnGet()
        {
            HomePageData = await _homePageService.GetData();
        }
    }
}

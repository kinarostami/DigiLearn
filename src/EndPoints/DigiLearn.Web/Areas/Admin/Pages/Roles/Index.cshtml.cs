using Common.Application;
using DigiLearn.Web.Infrastructure.RazorUtils;
using DigiLearn.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using UserModule.Core.Commands.Roles.Edit;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Services;
using UserModules.Core.Commands.Roles.Create;
using UserModules.Data.Entities._Enums;
using UserModules.Data.Entities.Roles;

namespace DigiLearn.Web.Areas.Admin.Pages.Roles;

public partial class IndexModel : BaseRazor
{
    private readonly IRoleFacade _roleFacade;
    private readonly IRenderViewToString _renderViewToString;

    public IndexModel(IRoleFacade roleFacade, IRenderViewToString renderViewToString)
    {
        _roleFacade = roleFacade;
        _renderViewToString = renderViewToString;
    }


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public string Name { get; set; }
    
    public List<Role> Roles { get; set; }

    public async Task OnGet()
    {
        Roles = await _roleFacade.GetAllRoles();
    }

    public async Task<IActionResult> OnPost(List<Permissions> permissions)
    {
        return await AjaxTryCatch(async () =>await _roleFacade.Create(new CreateRoleCommand()
        {
            Name = Name,
            Permissions = permissions  
        }));
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        return await AjaxTryCatch(() => _roleFacade.Delete(id));
    }

    public async Task<IActionResult> OnGetShowEditPage(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var role = await _roleFacade.GetRoleById(id);
            if (role == null)
                return OperationResult<string>.NotFound();

            var view = await _renderViewToString.RenderToStringAsync("_edit", new EditRoleViewModel()
            {
                Name = role.Name,
                RoleId = role.Id,
                SelectedPermissions = role.Permissions.Select(x => x.Permissions).ToList()
            },PageContext);
            return OperationResult<string>.Success(view);
        });
    }

    public async Task<IActionResult> OnPostEdit(EditRoleViewModel viewModel,List<Permissions> permissions)
    {
        return await AjaxTryCatch(() => _roleFacade.Edit(new EditRoleCommand()
        {
            Name = viewModel.Name,
            RoleId = viewModel.RoleId,
            Permissions = permissions
        }));
    }
}

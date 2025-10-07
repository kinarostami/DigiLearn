using System.ComponentModel.DataAnnotations;
using UserModules.Data.Entities._Enums;

namespace DigiLearn.Web.ViewModels
{
    public class EditRoleViewModel
    {
        public Guid RoleId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Name { get; set; }
        public List<Permissions> SelectedPermissions { get; set; } = new();
    }
}

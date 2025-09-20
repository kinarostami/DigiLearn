using Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModules.Data.Entities.Roles;
 
public class Role : BaseEntity
{
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }
    public List<RolePermission> Permissions { get; set; }
}

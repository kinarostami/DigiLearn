using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModules.Data.Entities._Enums;

namespace UserModules.Data.Entities.Roles;

public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public Permissions Permissions { get; set; }

    public Role Role { get; set; }
}

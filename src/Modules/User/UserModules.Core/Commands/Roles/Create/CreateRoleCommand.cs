using Common.Application;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModules.Data.Entities._Enums;

namespace UserModules.Core.Commands.Roles.Create;

public class CreateRoleCommand : IBaseCommand
{
    public string Name { get; set; }
    public List<Permissions> Permissions { get; set; } = new();
}

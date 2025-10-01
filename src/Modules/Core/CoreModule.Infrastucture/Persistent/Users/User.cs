using Common.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Infrastucture.Persistent.Users;

[Index("Email", IsUnique = true)]
[Index("PhoneNumber", IsUnique = true)]
public class User : BaseEntity
{
    [MaxLength(50)]
    public string? Name { get; set; }

    [MaxLength(50)]
    public string? Family { get; set; }

    [MaxLength(12)]
    public string PhoneNumber { get; set; }

    [MaxLength(110)]
    public string? Email { get; set; }

    [MaxLength(110)]
    public string? Avatar { get; set; }
}

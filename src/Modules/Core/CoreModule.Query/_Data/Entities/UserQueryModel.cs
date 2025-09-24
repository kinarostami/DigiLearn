using Common.Domain;
using CoreModule.Domain.Course.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query._Data.Entities;

[Table("Users",Schema = "dbo")]
public class UserQueryModel : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(50)]
    public string Family { get; set; }

    [MaxLength(12)]
    public string PhoneNumber { get; set; }

    [MaxLength(110)]
    public string Email { get; set; }

    [MaxLength(110)]
    public string Avatar { get; set; }
}

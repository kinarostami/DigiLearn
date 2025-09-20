using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogModules.Domain;

class Category : BaseEntity
{
    public string Title { get; set; }
    public string Slug { get; set; }
}

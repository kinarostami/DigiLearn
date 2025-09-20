using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogModules.Service.DTOs.Query
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public string OwnerName { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string ImageName { get; set; }
        public long Visit { get; set; }
        public Guid CategoryId { get; set; }
    }
}

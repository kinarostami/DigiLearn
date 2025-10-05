using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogModules.Service.DTOs.Command
{
    public class CreatePostCommand
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public string Slug { get; set; }
        public string OwnerName { get; set; }
        public string Descriptoin { get; set; }
        public IFormFile ImageFile { get; set; }
        public Guid CategoryId { get; set; }
    }
    public class EditPostCommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string OwnerName { get; set; }
        public string Descriptoin { get; set; }
        public IFormFile? ImageFile { get; set; }
        public Guid CategoryId { get; set; }
    }
}

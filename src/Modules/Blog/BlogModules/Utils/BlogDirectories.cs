using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogModules.Utils
{
    public class BlogDirectories
    {
        public static string PostImage = "wwwroot/blog/images";
        public static string GetPostImage(string imageName)
        {
            return $"{PostImage.Replace("wwwroot", "")}/{imageName}";
        }
    }
}

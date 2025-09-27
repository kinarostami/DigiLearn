using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application._Utils;

public class CoreModuleDirectories
{
    public static string CvFileName = "wwwroor/core/teacher";
    public static string CourseImage = "wwwroor/core/course";
    public static string CourseDemoVideo(Guid courseId) => $"wwwroor/core/course/{courseId}";

    public static string GetCourseImage(string imageName) => $"{CourseImage.Replace("wwwroot", "")}/{imageName}";
}

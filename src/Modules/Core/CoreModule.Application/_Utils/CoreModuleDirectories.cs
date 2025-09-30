﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application._Utils;

public class CoreModuleDirectories
{
    public static string CvFileName = "wwwroot/core/teacher";
    public static string CourseImage = "wwwroot/core/course";
    public static string CourseDemoVideo(Guid courseId) => $"wwwroot/core/course/{courseId}";
    public static string CourseEpisode(Guid courseId,Guid episodeToken) => $"wwwroot/core/course/{courseId}/episodes/{episodeToken}";

    public static string GetCourseImage(string imageName) => $"{CourseImage.Replace("wwwroot", "")}/{imageName}";

    public static string GetEpisodeFile(Guid courseId, Guid episodeToken,string fileName) 
        => $"{CourseEpisode(courseId,episodeToken).Replace("wwwroot", "")}/{fileName}";
}

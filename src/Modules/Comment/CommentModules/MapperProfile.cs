using AutoMapper;
using CommentModules.Domain;
using CommentModules.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentModules;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateCommentCommand, Comment>().ReverseMap();

    }
}

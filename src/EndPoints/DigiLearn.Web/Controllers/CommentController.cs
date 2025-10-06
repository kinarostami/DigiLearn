﻿using CommentModules.Domain;
using CommentModules.Services;
using CommentModules.Services.DTOs;
using DigiLearn.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DigiLearn.Web.Controllers;

public class CommentController : BaseController
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentCommand command)
    {
        command.UserId = User.GetUserId();
        return await AjaxTryCatch( () => _commentService.CreateComment(command));  
    }

    [Route("/comment/getByFilter")]
    public async Task<IActionResult> GetComments([FromQuery] Guid entityId,[FromQuery] CommentType commentType)
    {
        var modle = await _commentService.GetCommentByFilter(new CommentFilterParams()
        {
            EntityId = entityId,
            CommentType = commentType
        });
        return PartialView("Shared/Comments/_Comments",modle);
    }
}

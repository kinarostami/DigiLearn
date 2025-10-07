﻿using CommentModules.Domain;
using CommentModules.Services;
using CommentModules.Services.DTOs;
using Common.Application;
using DigiLearn.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IActionResult> CreateComment(CreateCommentCommand command)
    {
        command.UserId = User.GetUserId();
        return await AjaxTryCatch( () => _commentService.CreateComment(command));  
    }

    [Route("/comment/getByFilter")]
    public async Task<IActionResult> GetComments([FromQuery] Guid entityId,[FromQuery] CommentType commentType,int pageId = 1)
    {
        var modle = await _commentService.GetCommentByFilter(new CommentFilterParams()
        {
            EntityId = entityId,
            CommentType = commentType,
            Take = 2,
            PageId = pageId
        });
        return PartialView("Comments/_Comments",modle);
    }

    [HttpPost("/comment/delete")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        var comment = await _commentService.GetCommentById(commentId);
        if (comment == null || comment.UserId != User.GetUserId())
        {
            return await AjaxTryCatch(() => Task.FromResult(OperationResult.NotFound()));
        }
        return await AjaxTryCatch(() =>  _commentService.DeleteComment(commentId));
    }
}

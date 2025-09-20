using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Core;
using UserModules.Data;

namespace UserModules.Core.Commands.Users.ChangeAvatar
{
    public class ChangeUserAvatarCommand : IBaseCommand
    {
        public Guid UserId { get; set; }
        public IFormFile AvatarFile { get; set; }
    }
    public class ChangeUserAvatarCommandHandler : IBaseCommandHandler<ChangeUserAvatarCommand>
    {
        private readonly UserContexts _userContexts;
        private readonly ILocalFileService _localFileService;

        public ChangeUserAvatarCommandHandler(UserContexts userContexts, ILocalFileService localFileService)
        {
            _userContexts = userContexts;
            _localFileService = localFileService;
        }

        public async Task<OperationResult> Handle(ChangeUserAvatarCommand request, CancellationToken cancellationToken)
        {
            var user = await _userContexts.Users.FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);
            if (user == null)
                return OperationResult.NotFound();

            if (request.AvatarFile.IsImage() == false)
                return OperationResult.Error("عکس نانعتبر است");

            var avatar =
           await _localFileService.SaveFileAndGenerateName(request.AvatarFile, UserModuleDirectories.UserAvatar);
            user.Avatar = avatar;
            _userContexts.Users.Update(user);
            await _userContexts.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }
    }
}

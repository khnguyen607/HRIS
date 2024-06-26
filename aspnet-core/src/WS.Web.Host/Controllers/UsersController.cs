﻿using Abp.AspNetCore.Mvc.Authorization;
using WS.Authorization;
using WS.Storage;
using Abp.BackgroundJobs;

namespace WS.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}
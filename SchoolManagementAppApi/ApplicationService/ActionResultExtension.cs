using Microsoft.AspNetCore.Mvc;
using Utilities.Result.Util;

namespace SchoolManagementAppApi.ApplicationService
{
    public static class ActionResultExtension
    {
        public static IActionResult ResponseResult(this Utilities.Result.Util.ActionResult response)
        {
            return response.Code switch
            {
                ErrorCode.BadRequest => new BadRequestObjectResult(response),
                ErrorCode.NotFound => new NotFoundObjectResult(response),
                ErrorCode.UnAuthorized => new UnauthorizedObjectResult(response),
                ErrorCode.InternalServerError => new ObjectResult(response),
                ErrorCode.Ok => new OkObjectResult(response),
                _ => new OkObjectResult(response),
            };
        }
    }
}

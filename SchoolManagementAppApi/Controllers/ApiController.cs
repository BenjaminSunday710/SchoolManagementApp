using Microsoft.AspNetCore.Mvc;
using Shared.Application.Mediator;

namespace SchoolManagementAppApi.Controllers
{
    [ApiController]
    public class ApiController:ControllerBase
    {
        public ApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
        protected IMediator Mediator { get; }
    }
}

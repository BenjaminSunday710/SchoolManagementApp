using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Application.Mediator;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers
{
    [Route("states")]
    public class UtilController:ApiController
    {
        private IOptions<NigeriaStates_and_LgasOptions> _options;

        public UtilController(IMediator mediator, IOptions<NigeriaStates_and_LgasOptions> options) 
            : base(mediator) 
        {
            _options = options;
        }

        [HttpGet]
        public async Task<IActionResult> FetchStates()
        {
            return Ok(Task.Run(() =>
            {
                return _options.Value.States;

            }));
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> FetchStateLgas(string name)
        {
            return Ok(Task.Run(() =>
            {
                var state = _options.Value.States.FirstOrDefault(x => x.Name == name);
                return state.Lgas;
            }));
        }
    }

    public class NigeriaStates_and_LgasOptions
    {
        public List<State> States { get; set; }
    }

    public class State
    {
        public string Name { get; set; }
        public List<string> Lgas { get; set; }
    }
}

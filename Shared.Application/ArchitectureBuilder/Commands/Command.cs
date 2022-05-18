using Utilities.Result.Util;

namespace Shared.Application.ArchitectureBuilder.Commands
{
    public abstract class Command:ICommand
    {
        protected abstract ActionResult Validate();

        public bool IsValid => (Validate().WasSuccessful) ? true : false;
    }
}

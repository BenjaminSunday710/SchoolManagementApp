using Utilities.Result.Util;

namespace Shared.Application.ArchitectureBuilder.Queries
{
    public abstract class Query
    {
        protected abstract ActionResult Validate();
        public bool IsValid => (Validate().WasSuccessful) ? true : false;
    }
}

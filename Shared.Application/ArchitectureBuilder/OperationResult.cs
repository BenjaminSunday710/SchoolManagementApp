using System.Collections.Generic;
using Utilities.Result.Util;

namespace Shared.Application.ArchitectureBuilder
{
    public class OperationResult<TResponse>
    {
        public ActionResult<TResponse> Failed(string error)
        {
            return ActionResult<TResponse>.Failed()
                .AddError(error);
        }

        public ActionResult<TResponse> Failed(List<string> errors)
        {
            return ActionResult<TResponse>.Failed()
                .SetErrors(errors);
        }

        public ActionResult<TResponse> Successful(TResponse response)
        {
            return ActionResult<TResponse>.Success(response);
        }

        public ActionResult<List<TResponse>> Successful(List<TResponse> response)
        {
            return ActionResult<List<TResponse>>.Success(response);
        }

        public ActionResult<List<TResponse>> Failed(string error, bool isReturnTypeCollection = true)
        {
            return ActionResult<List<TResponse>>.Failed()
               .SetErrors(new List<string>() { error });
        }

        protected ActionResult<TRequiredResponse> Successful<TRequiredResponse>(TRequiredResponse response)
        {
            return ActionResult<TRequiredResponse>.Success(response);
        }

        protected ActionResult<TRequiredResponse> Failed<TRequiredResponse>(string error)
        {
            return ActionResult<TRequiredResponse>.Failed()
                .SetErrors(new List<string>() { error });
        }
    }
}

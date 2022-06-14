using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Results.FetchResultVariantManager
{
    public class FetchResultVariantManagerQueryHandler : QueryHandler<ResultVariantManager, Guid, ResultVariantManagerResponse, FetchResultVariantManagerQuery>
    {
        public override async Task<ActionResult<ResultVariantManagerResponse>> HandleAsync(FetchResultVariantManagerQuery query, CancellationToken cancellationToken = default)
        {
            var resultVariantManagers = await QueryContext.GetAllAsync();
            var resultVariantManager = resultVariantManagers.FirstOrDefault(x => x.Session == query.Session && x.Term == query.Term);
            var response = new ResultVariantManagerResponse
            {
                Id = resultVariantManager.Id,
                Session = resultVariantManager.Session,
                Term = resultVariantManager.Term
            };
            return OperationResult.Successful(response);
        }
    }
}

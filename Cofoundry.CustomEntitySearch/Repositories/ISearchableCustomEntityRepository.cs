using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Domain
{
    public interface ISearchableCustomEntityRepository : ICustomEntityRepository
    {
        Task<PagedQueryResult<CustomEntityRenderSummary>> SearchCustomEntityRenderSummariesAsync(SearchCustomEntitiesQuery query, IExecutionContext executionContext = null);

    }
}

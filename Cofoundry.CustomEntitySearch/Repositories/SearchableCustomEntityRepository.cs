using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.CustomEntitySearch.Repositories
{
    public class SearchableCustomEntityRepository : CustomEntityRepository, ISearchableCustomEntityRepository
    {
        private readonly IQueryExecutor _queryExecutor;

        public SearchableCustomEntityRepository(IQueryExecutor queryExecutor,
            ICommandExecutor commandExecutor) : base(queryExecutor, commandExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public Task<PagedQueryResult<CustomEntityRenderSummary>> SearchCustomEntityRenderSummariesAsync(SearchCustomEntitiesQuery query, IExecutionContext executionContext = null)
        {
            return _queryExecutor.ExecuteAsync(query, executionContext);
        }
    }
}

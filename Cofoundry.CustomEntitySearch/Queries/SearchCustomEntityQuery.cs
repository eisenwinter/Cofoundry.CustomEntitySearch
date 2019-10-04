using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cofoundry.Domain
{
    public class SearchCustomEntitiesQuery : SimplePageableQuery
        , IQuery<PagedQueryResult<CustomEntityRenderSummary>>
    {


        [MaxLength(6)]
        [Required]
        public string CustomEntityDefinitionCode { get; set; }

        public SortDirection? SortDirection { get; set; }

        public CustomEntityQuerySortType SortBy { get; set; }


        /// <summary>
        /// Locale id to filter the results by, if null then only entities
        /// with a null locale are shown
        /// </summary>
        public int? LocaleId { get; set; }

        public PublishStatusQuery PublishStatus { get; set; }

        public IEnumerable<ICustomEntitySearchSpecification> Specifications { get; set; }

    }

}

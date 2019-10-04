using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cofoundry.Domain
{
    public interface ISearchSpecificationMapper<TOut>
    {
        TOut Map(IEnumerable<ICustomEntitySearchSpecification> specifications);
    }
}

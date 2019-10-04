using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cofoundry.Domain
{
    public abstract class CustomEntitySearchSpecificationBase<T> : ICustomEntitySearchSpecification<T> where T : ICustomEntityDataModel
    {
        public abstract Expression<Func<T, bool>> SatisfiedBy { get; }

        public Expression RawExpression => SatisfiedBy;
    }
}

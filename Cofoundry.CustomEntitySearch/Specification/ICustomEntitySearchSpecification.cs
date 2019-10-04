using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cofoundry.Domain
{
    public interface ICustomEntitySearchSpecification
    {
        Expression RawExpression { get; }
    }
    public interface ICustomEntitySearchSpecification<T> : ICustomEntitySearchSpecification where T : ICustomEntityDataModel
    {
        Expression<Func<T, bool>> SatisfiedBy { get; }
    }
}

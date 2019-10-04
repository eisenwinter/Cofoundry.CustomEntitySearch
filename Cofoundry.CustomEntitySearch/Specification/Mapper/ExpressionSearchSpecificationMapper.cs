using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cofoundry.Domain
{
    class ExpressionSearchSpecificationMapper : ISearchSpecificationMapper<Expression>
    {

        public Expression Map(IEnumerable<ICustomEntitySearchSpecification> specifications)
        {
            var result = specifications.FirstOrDefault();
            if(result != null)
            {
                if (specifications.Count() == 1) return result.RawExpression;
                var expression = ((LambdaExpression)result.RawExpression).Body;
                var parameter = ((LambdaExpression)result.RawExpression).Parameters.Single();
                foreach(var spec in specifications.Skip(1))
                {
                    expression = Expression.AndAlso(((BinaryExpression)expression), ((LambdaExpression)spec.RawExpression).Body);
                }
                expression = Expression.Lambda(expression, parameter);
                var visitor = new ParameterSubstituteVisitor(parameter);
                return visitor.Visit(expression);
            }
            return null;
        }

        public class ParameterSubstituteVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression parameterExpression;
            public ParameterSubstituteVisitor(ParameterExpression par)
            {
                this.parameterExpression = par;
            }

            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                return Expression.Lambda(Visit(node.Body), parameterExpression);
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return parameterExpression;
            }

        }

    }
}

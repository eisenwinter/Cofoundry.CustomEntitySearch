using Cofoundry.Core.DependencyInjection;
using Cofoundry.CustomEntitySearch.Repositories;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cofoundry.Domain
{
    public class CustomSearchDependecyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                 .Register<CofoundryDbContextWithJsonSupport>(new Type[] { typeof(CofoundryDbContext), typeof(DbContext) }, RegistrationOptions.Scoped())
                 .Register<ISearchableCustomEntityRepository, SearchableCustomEntityRepository>()
                 .Register<ISearchSpecificationMapper<Expression>, ExpressionSearchSpecificationMapper>();
        }
    }
}

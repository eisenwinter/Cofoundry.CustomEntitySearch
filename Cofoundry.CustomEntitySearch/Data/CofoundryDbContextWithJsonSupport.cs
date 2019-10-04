using Cofoundry.Core.EntityFramework;
using Cofoundry.CustomEntitySearch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cofoundry.Domain.Data
{
    public class CofoundryDbContextWithJsonSupport : CofoundryDbContext
    {
        public CofoundryDbContextWithJsonSupport(ICofoundryDbContextInitializer cofoundryDbContextInitializer) : base(cofoundryDbContextInitializer)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDbFunction(typeof(SqlServerJsonExtension)
                                .GetMethod(nameof(SqlServerJsonExtension.JsonValue)))
                    .HasSchema("")
                    .HasName("JSON_VALUE");
        }
    }

}
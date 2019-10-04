using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cofoundry.CustomEntitySearch
{
    internal static class SqlServerJsonExtension
    {
        public static string JsonValue(string column, [NotParameterized] string path)
        {
            throw new NotSupportedException();
        }
    }
}

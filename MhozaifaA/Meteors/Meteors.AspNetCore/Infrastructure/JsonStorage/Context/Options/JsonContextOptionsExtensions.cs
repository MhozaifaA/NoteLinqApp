using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Infrastructure.JsonStorage
{
    public static class JsonContextOptionsExtensions
    {
        public static JsonContextConfigure SetConnectionString(this JsonContextConfigure configure,
            string connectionString)
        {
            configure.ConnectionString = connectionString;
            return configure;
        }

        public static JsonContextConfigure SetDataBaseName(this JsonContextConfigure configure,
           string dataBaseName)
        {
            configure.DataBaseName = dataBaseName;
            return configure;
        }

    }
}

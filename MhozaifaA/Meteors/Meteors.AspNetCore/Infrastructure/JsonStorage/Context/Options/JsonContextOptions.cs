using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Infrastructure.JsonStorage
{
    public class JsonContextOptions
    {
        public JsonContextConfigure Configure { get; set; }

        public void Insure()
        {
            if(Configure is null)
                Configure = new JsonContextConfigure();
        }
    }

    public class JsonContextConfigure
    {

        /// <summary>
        /// Folder path to store json collecitons
        /// </summary>
        internal string ConnectionString { get; set; }

        /// <summary>
        /// Folder Name to store json collecitons 
        /// </summary>
        internal string DataBaseName { get; set; }

        public string gConnectionString => Path.Combine(ConnectionString, DataBaseName);
    }

}

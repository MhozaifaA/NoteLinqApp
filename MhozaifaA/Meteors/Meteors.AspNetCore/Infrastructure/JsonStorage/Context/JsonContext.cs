
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Infrastructure.JsonStorage
{
    public abstract class JsonContext
    {
        protected readonly JsonContextOptions Optinos;
        public JsonContext(JsonContextOptions options)
        {
            Optinos = options;
            Optinos.Insure();
            OnConfigure(Optinos.Configure);
        }

        protected abstract void OnConfigure(JsonContextConfigure Configure);

        protected string GetConnectionString() => Optinos.Configure.ConnectionString;

        public abstract void SaveChanges();
        public abstract ValueTask SaveChangesAsync();
    }
}

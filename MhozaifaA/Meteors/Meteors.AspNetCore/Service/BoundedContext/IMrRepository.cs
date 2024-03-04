using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Service.BoundedContext
{
    public interface IMrRepository { }
    public interface IMrRepository<TKey>: IMrRepository where TKey : struct, IEquatable<TKey> { }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Security.Identity
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class IdentityGenerationStamp : IIdentityGenerationStamp
    {
        public string GenerationStamp { get; set; }
    }

    /// <summary>
    /// Identity uniq for jwt token generation from once login until to new generation
    /// </summary>
    public interface IIdentityGenerationStamp
    {
        public string GenerationStamp { get; set; }
    }
}

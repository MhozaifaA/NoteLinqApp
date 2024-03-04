using Meteors.AspNetCore.Common.Services.Verification.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.Verification
{
    public interface IMrVerification
    {
        OtpVia Generate(string id);
        OtpVia Generate(string id, out DateTime expire);
        OtpVia Generate(string id, MrVerificationOptions option);
        OtpVia Generate(string id, int expireMin, out DateTime expire);
        OtpVia Generate(string id, MrVerificationOptions option, out DateTime expire);
        bool Scan(string id, string plain);
        bool Scan(string id, string plain, MrVerificationOptions option);
        bool Scan(string id, string plain, int expire);
        bool Scan(string url);
        bool Scan(string url, out string id, out string code);
        bool Scan(string url, int expire, out string id, out string code);
    }
}

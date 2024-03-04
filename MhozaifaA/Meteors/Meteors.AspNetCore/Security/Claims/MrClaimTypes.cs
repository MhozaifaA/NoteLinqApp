using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Security.Claims
{
    public static class MrClaimTypes
    {
        public const string Role = ClaimTypes.Role;
        public const string NameIdentifier = ClaimTypes.NameIdentifier;
        public const string UserData = ClaimTypes.UserData;
        public const string TransferredProps = "transferred-prop";
        public const string GenerationStamp = "generation-stamp";
        public const string GenerateDate = "generate-date";
        public const string UserId = "user-id";
        public const string RefreshToken = "refresh-token";
        public const string Token = "Authorization";
    }
}

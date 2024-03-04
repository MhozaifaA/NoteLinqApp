using Meteors.AspNetCore.Helper.ExtensionMethods.String;
using Meteors.AspNetCore.Security.Claims;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Meteors.AspNetCore.Security.Claims
{
    public static class ClaimsPrincipalExtension
    {

        public static object? CurrentUserId(this System.Security.Claims.ClaimsPrincipal user) =>
             user.FindFirst(MrClaimTypes.NameIdentifier)?.Value??null;

        public static T? CurrentUserId<T>(this System.Security.Claims.ClaimsPrincipal user)
        {
            var obId = user.FindFirst(MrClaimTypes.NameIdentifier);
            if (obId is null)
                return default(T);
            return obId.Value.ChangeType<T>();
        }

        public static T CurrentUserId<T>(this IEnumerable<Claim> claims)
            => (T)Convert.ChangeType(claims.FirstOrDefault(claim => claim.Type == (MrClaimTypes.NameIdentifier))?.Value, typeof(T));

        public static T CurrentUserRole<T>(this IEnumerable<Claim> claims) =>
            claims.First(claim => claim.Type == (MrClaimTypes.Role)).Value.ToEnum<T>();

        public static T CurrentUserData<T>(this System.Security.Claims.ClaimsPrincipal user)
          => user.FindFirst(MrClaimTypes.UserData).Value.ChangeType<T>();

        public static TransferredProp CurrentTransferredProps(this System.Security.Claims.ClaimsPrincipal user)
         => JsonSerializer.Deserialize<TransferredProp>(user.FindFirst(MrClaimTypes.TransferredProps).Value);

        public static DateTime CurrentGenerationStamp(this System.Security.Claims.ClaimsPrincipal user)
         => user.FindFirst(MrClaimTypes.GenerationStamp).Value.ChangeType<DateTime>();

        public static DateTime CurrentGenerateDate(this System.Security.Claims.ClaimsPrincipal user)
        => user.FindFirst(MrClaimTypes.GenerateDate).Value.ChangeType<DateTime>();

        public static string? CurrentToken(this System.Security.Claims.ClaimsPrincipal user)
       => user.FindFirst(MrClaimTypes.Token)?.Value;

    }
}

using Meteors.AspNetCore.Infrastructure.ModelEntity.Securty;
using Meteors.AspNetCore.Security.Claims;
using Meteors.AspNetCore.Security.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Security.JWT
{
    public class JwtBearerAuthentication : IJwtBearerAuthentication
    {
        readonly JwtBearerAuthenticationOptions Options;
        public JwtBearerAuthentication(IOptions<JwtBearerAuthenticationOptions> options)
        {
            Options = options?.Value;
        }

        public IEnumerable<Claim> SetNameIdentifier(string id)
        {
           yield return new Claim(MrClaimTypes.NameIdentifier, id);
        }

        public string GenerateJwtToken<TUser,TNameIdentifier>(TUser user) where TNameIdentifier : struct, IEquatable<TNameIdentifier>
            where TUser : MrIdentityUser<TNameIdentifier>, IIdentityGenerationStamp
        {
            var claims = new List<Claim>
            {
                new Claim(MrClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(MrClaimTypes.GenerateDate, DateTime.Now.ToLocalTime().ToString()),
                new Claim(MrClaimTypes.GenerationStamp, user.GenerationStamp ),
                //new Claim(MrClaimTypes.TransferredProps, (string)new TransferredProp() ),
            };

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(MrClaimTypes.Role, role));
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(Options.Issuer,
                  Options.Audience,
                  claims,
                  expires: DateTime.Now.AddMinutes(Options.ExpireMinut),
                  signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateJwtToken<TUser>(TUser user) where TUser : MrIdentityUser<Guid>, IIdentityGenerationStamp
        {
            var claims = new List<Claim>
            {
                new Claim(MrClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(MrClaimTypes.GenerateDate, DateTime.Now.ToLocalTime().ToString()),
                new Claim(MrClaimTypes.GenerationStamp, user.GenerationStamp ),
                //new Claim(MrClaimTypes.TransferredProps, (string)new TransferredProp() ),
            };

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(MrClaimTypes.Role, role));
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(Options.Issuer,
                  Options.Audience,
                  claims,
                  expires: DateTime.Now.AddMinutes(Options.ExpireMinut),
                  signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}

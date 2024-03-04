using Meteors.AspNetCore.Security.Claims;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Resolver
{
    /// <summary>
    /// <para>resolver Interface wide use</para>
    /// Inject <see cref="HttpContextAccessor"/> enabled resolver http and user to more specific
    /// </summary>
    public interface IHttpResolverService
    {
        /// <summary>
        /// Encapsulates all HTTP-specific information about an individual HTTP request.
        /// </summary>
        HttpContext HttpContext { get; }
        string AppBaseUrl { get; }

        /// <summary>
        /// Return lang in url like: http://host:port/api/{lang}/....
        /// </summary>
        /// <returns><see cref="string"/></returns>
        string Lang();
        /// <summary>
        /// Return user-id content in Authorize header by <see cref="MrClaimTypes.NameIdentifier"/>.
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <returns><see cref="string"/></returns>
        Tkey? GetCurrentUserId<Tkey>() where Tkey : struct, IEquatable<Tkey>;
        /// <summary>
        /// Return token-data content in Authorize header by <see cref="MrClaimTypes.TransferredProps"/>.
        /// </summary>
        /// <returns><see langword="null"/> or <see cref="TransferredProp"/></returns>
        TransferredProp? GetCurrentTransferredProps();
        /// <summary>
        /// Gets or sets the user for this request.
        /// </summary>
        /// <returns><see cref="ClaimsPrincipal"/></returns>
        ClaimsPrincipal GetCurrentUserContext();
        /// <summary>
        /// Check Authorize header  User?.Identity?.IsAuthenticated.
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        bool IsAuthenticated();
        string BaseUrl();
        string GetCurrentUserName();
    }
}

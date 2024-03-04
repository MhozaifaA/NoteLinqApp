
/// <summary>
/// ---------------------------------------------
/// | ::Mr:: AddMrRepositoryInject version 1.1.0 |
/// ---------------------------------------------
/// </summary>
namespace Meteors.AspNetCore.Service.DependencyInjection
{
    /// <summary>
    /// DependencyInjection kind of lifetime object 
    /// <para>uses with <see cref="Meteors.AspNetCore.Service.DependencyInjection.MrRepositoryAttribute"/> .</para>
    /// <para> Remarks this <see langword="enum"/> same <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime"/> </para>
    /// </summary>
    public enum Lifetimes
    {
        /// <summary>
        /// <see cref="IServiceCollection.AddSingleton()"/>
        /// <para>Specifies that a single instance of the service will be created.</para>
        /// </summary>
        Singleton,
        /// <summary>
        /// <see cref="IServiceCollection.AddScoped()"/>
        /// <para>
        /// Specifies that a new instance of the service will be created for each scope.
        /// In ASP.NET Core applications a scope is created around each server request.
        /// </para>
        /// </summary>
        Scoped,
        /// <summary>
        /// <see cref="IServiceCollection.AddTransient()"/>
        /// <para>Specifies that a new instance of the service will be created every time it is requested.</para>
        /// </summary>
        Transient,
    }
    
}

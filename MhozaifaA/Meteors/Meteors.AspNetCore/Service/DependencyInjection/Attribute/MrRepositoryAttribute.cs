using Meteors.AspNetCore.Helper.ExtensionMethods.String;
using System;
using System.Linq;

/// <summary>
/// ---------------------------------------------
/// | ::Mrkood:: AddMrRepositoryInject version 1.1.0 |
/// ---------------------------------------------
/// </summary>
namespace Meteors.AspNetCore.Service.DependencyInjection
{
    /// <summary>
    /// Custom attribute uses to inject all Repositories .
    /// <para>Warning: should inhernet all repositories from <see langword="I"/>[Name your repo] </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MrRepositoryAttribute : Attribute
    {
        /// <summary>
        /// Inner lifetime  .
        /// </summary>
        public Lifetimes LifetimeTypes { get; private set; }

        /// <summary>
        /// Defult constructor pass not parameters inject <see cref="Lifetimes.Scoped"/> .
        /// </summary>
        public MrRepositoryAttribute() => LifetimeTypes = Lifetimes.Scoped;

        /// <summary>
        /// Defult constructor pass <see cref="Lifetimes"/> .
        /// </summary>
        /// <param name="lifetime"></param>
        public MrRepositoryAttribute(Lifetimes lifetime) => LifetimeTypes = lifetime;

        /// <summary>
        /// Help constructor pass <see cref="string"/> of typo <see cref="lifetime"/> .
        /// </summary>
        /// <param name="lifetime"></param>
        public MrRepositoryAttribute(string lifetime):this(lifetime.ToEnum<Lifetimes>()){}

        /// <summary>
        /// Reflection get <see cref="Lifetimes"/> which used with Repositories .
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Lifetimes GetLifetime(Type type)
            => type.GetCustomAttributes(typeof(MrRepositoryAttribute), false)
                            .Cast<MrRepositoryAttribute>().Select(x => x.LifetimeTypes)
                            .Single();
    }
}

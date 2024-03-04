using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Session
{
    public class MrSessionOptions
    {
        /// <summary>
        /// Default value 20 minutes
        /// </summary>
        internal TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(20);
        /// <summary>
        /// Default value <see cref="SupportedSessions.AspNet"/>
        /// </summary>
        internal SupportedSessions SupportedSession { get; set; } = SupportedSessions.AspNet;

        /// <summary>
        /// Enable compress default value True
        /// </summary>
        internal bool Gzip { get; set; } = true;

        /// <summary>
        /// Return <see cref="InitSessionName"/> by <see cref="SupportedSession"/>
        /// </summary>
        /// <returns></returns>
        public string BackSessionName => InitSessionName.Invoke(SupportedSession);

        /// <summary>
        /// init default sessions name, build with <see cref="BackSessionName"/> to optional re init as need name
        /// <para> SupportedSessions.AspNet => ".Meteors.AspNet.Session", </para>
        /// <para> SupportedSessions.Protect => ".Meteors.Protect.Session",</para>
        /// <para> SupportedSessions.Unsafe => ".Meteors.Unsafe.Session", </para>
        /// <para> SupportedSessions.Page => ".Meteors.Page.Session", </para>
        /// </summary>
        public Func<SupportedSessions, string> InitSessionName = (SupportedSession)
              => SupportedSession switch
              {
                  SupportedSessions.AspNet => ".Meteors.AspNet.Session",
                  SupportedSessions.Protect => ".Meteors.Protect.Session",
                  SupportedSessions.Unsafe => ".Meteors.Unsafe.Session",
                  SupportedSessions.Page => ".Meteors.Page.Session",
              };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Session
{
    public static class elsessionOptionExtensions
    {
        public static MrSessionOptions Timeout(this MrSessionOptions option, TimeSpan timeout)
        {
            option.Timeout = timeout;
            return option;
        }

        public static MrSessionOptions TimeoutFromMinutes(this MrSessionOptions option, double timeout)
        {
            option.Timeout = TimeSpan.FromMinutes(timeout);
            return option;
        }

        public static MrSessionOptions SupportedSession(this MrSessionOptions option, SupportedSessions supported)
        {
            option.SupportedSession = supported;
            return option;
        }

        public static MrSessionOptions AspNetSession(this MrSessionOptions option) => option.SupportedSession(SupportedSessions.AspNet);

        public static MrSessionOptions ProtectSession(this MrSessionOptions option) => option.SupportedSession(SupportedSessions.Protect);

        public static MrSessionOptions UnsafeSession(this MrSessionOptions option) => option.SupportedSession(SupportedSessions.Unsafe);
        

        public static MrSessionOptions PageSession(this MrSessionOptions option) => option.SupportedSession(SupportedSessions.Page);


        /// <summary>
        /// Will affect off compression <see cref="SupportedSessions.Protect"/>
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public static MrSessionOptions DisableCompression(this MrSessionOptions option)
        {
            option.Gzip = false;
            return option;
        }

    }
}

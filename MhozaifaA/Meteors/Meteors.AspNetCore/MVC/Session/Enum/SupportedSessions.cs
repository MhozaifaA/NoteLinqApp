using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Session
{
    /// <summary>
    /// Types of Session Supported by Meteors.AspNetCore with more mechanism worked in core session
    /// Please visit this topics: Save Data Protectd, Http Session, HttpClientPage Session, Browsers Session, Temporary Data
    /// </summary>
    public enum SupportedSessions
    {
        /// <summary>
        /// Uses .AspNet.Session ServerSide
        /// </summary>
        AspNet,
        /// <summary>
        /// Custom protect data in ClientSide from ServerSide, enabled (compression, encrypted/decrypted, serialize)
        /// </summary>
        Protect,
        /// <summary>
        /// from ClinetSide , setSession/SessionStorage 
        /// </summary>
        Unsafe,
        /// <summary>
        /// User web page
        /// </summary>
        Page,
    }
}

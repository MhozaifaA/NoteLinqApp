using Meteors.AspNetCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.FCM
{
    /// <summary>
    /// //abstract
    /// </summary>
    public class  BaseDataFCM
    {
        /// <summary>
        /// DateTime.Now
        /// </summary>
        public DateTime FiringDate { get; private set; } = DateTime.Now;
    }
    public class DefaultDataFCM : BaseDataFCM, IKey
    {
        public string Key { get; set; }
    }
}

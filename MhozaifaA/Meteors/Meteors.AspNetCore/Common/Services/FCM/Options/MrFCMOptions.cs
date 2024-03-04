using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.FCM
{
    public class MrFCMOptions
    {
        public static readonly string Name = "fcm";
        internal string ApplicationId { get; set; }
        internal bool EnableBaseData { get; set; } = false;
    }
}

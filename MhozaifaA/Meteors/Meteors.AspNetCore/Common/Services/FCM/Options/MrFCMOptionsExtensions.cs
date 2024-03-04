using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.FCM
{
    public static class MrFCMOptionsExtensions
    {
        public static MrFCMOptions DefaultApplicationId(this MrFCMOptions options,string applicationId)
        {
            options.ApplicationId = applicationId;
            return options;
        }

        /// <summary>
        /// <see cref="DefaultDataFCM"/> 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MrFCMOptions EnableBaseData(this MrFCMOptions options)
        {
            options.EnableBaseData = true;
            return options;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Core.Secure
{
    public class MeteorsSecure
    {
        /// <summary>
        /// High secure key to all Meteors version will be static
        /// </summary>
        [Obsolete("Don't change at all")]
        public const string Meteors_Protect_Session = "8n^#;QZ?ll.-B656BACE-DQTHwtLNhm|-A50D-nX:=!xYaL}-HYE8-%]Le}+~SG}c^F&)vNP'b<-8224-chBC;q-le4RCZ9NSp-{<6UjX";
    }
}

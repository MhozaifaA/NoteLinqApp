using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Security.Claims
{
    public class TransferredProp : Dictionary<string,object> 
    {
        public static explicit operator string(TransferredProp value)
        {
            return  System.Text.Json.JsonSerializer.Serialize(value);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

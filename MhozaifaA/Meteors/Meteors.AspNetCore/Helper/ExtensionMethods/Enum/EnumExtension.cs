using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Helper.ExtensionMethods.Enum
{
    public static class EnumExtension
    {
        public static string[] ToStringArray<T>(this T[] arrayofenum) where T:System.Enum
            => arrayofenum.Select(x => x.ToString()).ToArray(); //.Cast<string>()

        public static string[] ToStringArray<T>() where T : System.Enum
           => ToStringArray(typeof(T));

        public static string[] ToStringArray(Type type)
        {
            if (!type.IsEnum)
                throw new ArgumentException($"{type} is not an System.Enum");
            return System.Enum.GetNames(type);
        }
            
    }
}

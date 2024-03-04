using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using Meteors.AspNetCore.Helper.ExtensionMethods.Enum;

namespace Meteors.AspNetCore.IO.Files
{
    public static class MrFileOptionExtentions
    {
        public static MrFileOption SetPath(this MrFileOption option, string path, MrFileType type)
        {
            option.Path = path;
            option.Type = type;
            return option;
        }

        public static MrFileOption Catagories(this MrFileOption option, Type type)
        {
            option.Categories = EnumExtension.ToStringArray(type);
            return option;
        }

        public static MrFileOption Catagories<T>(this MrFileOption option) where T : Enum
        {
            return Catagories(option, typeof(T));
        }
    }
}

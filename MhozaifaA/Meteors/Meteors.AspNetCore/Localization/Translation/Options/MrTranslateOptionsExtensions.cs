using Meteors.AspNetCore.Core.Culture;
using Meteors.AspNetCore.Helper.ExtensionMethods.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Localization.Translation.Options
{
    public static class MrTranslateOptionsExtensions
    {

        public static MrTranslateOptions DefaultLanguage(this MrTranslateOptions oprions, string code)
        {
            oprions.DefaultLanguageCode = code;
            return oprions;
        }

        public static MrTranslateOptions DefaultLanguage(this MrTranslateOptions oprions, LanguageCode code)
        {
            return oprions.DefaultLanguage(code.ToString());
        }

        public static MrTranslateOptions Language(this MrTranslateOptions oprions,params string[] codes)
        {
            oprions.LanguageCodes = codes;
            return oprions;
        }

        public static MrTranslateOptions Languages(this MrTranslateOptions oprions, params LanguageCode[] codes)
        {
            return oprions.Language(codes.ToStringArray());
        }

        public static MrTranslateOptions StopTranslator(this MrTranslateOptions oprions)
        {
            oprions.Enable = false;
            return oprions;
        }
    }
}

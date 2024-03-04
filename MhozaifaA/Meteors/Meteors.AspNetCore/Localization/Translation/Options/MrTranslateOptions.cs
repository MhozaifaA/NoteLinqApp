using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Localization.Translation.Options
{
    public class MrTranslateOptions
    {
        internal bool Enable { get; set; } = true;
        internal string[] LanguageCodes { get; set; }
        internal string DefaultLanguageCode { get; set; }
    }
}

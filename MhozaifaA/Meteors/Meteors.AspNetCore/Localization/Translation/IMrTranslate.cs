using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Localization.Translation
{
    public interface IMrTranslate
    {
        string GetDefaultLanguage();
        string[] GetLanguages();
        bool IsEnabled();
        string Translate(string text, string languagecode, bool isHtml = false);
        IEnumerable<string> Translate(IEnumerable<string> texts, string languagecode, bool isHtml = false);
        Dictionary<string, string> Translate(string text, bool isHtml = false);
        Dictionary<string, IEnumerable<string>> Translate(IEnumerable<string> texts, bool isHtml = false);
        ValueTask<string> TranslateAsync(string text, string languagecode, bool isHtml = false);
        ValueTask<IEnumerable<string>> TranslateAsync(IEnumerable<string> texts, string languagecode, bool isHtml = false);
        ValueTask<Dictionary<string, string>> TranslateAsync(string text, bool isHtml = false);
        ValueTask<Dictionary<string, IEnumerable<string>>> TranslateAsync(IEnumerable<string> texts, bool isHtml = false);
        ValueTask<Dictionary<string, List<string>>> TranslateAsync(IEnumerable<(string value, ITranslateAttribute info)> texts);
    }
}

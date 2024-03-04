using Meteors.AspNetCore.Localization.Translation.Options;
using Google.Cloud.Translation.V2;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Localization.Translation
{
    public class MrTranslate : IMrTranslate
    {
        private readonly MrTranslateOptions options;
        private readonly TranslationClient client;

        public MrTranslate(IOptions<MrTranslateOptions> options)
        {
            this.options = options.Value;
            client = TranslationClient.Create();
        }

        public bool IsEnabled() => options.Enable;
        public string[] GetLanguages() => options.LanguageCodes;
        public string GetDefaultLanguage() => options.DefaultLanguageCode;


        public string Translate(string text, string languagecode, bool isHtml = false)
        {
            if (!IsEnabled()) return default;

            TranslationResult response;

            if(isHtml) response = client.TranslateHtml(text, languagecode, options.DefaultLanguageCode);
            else response = client.TranslateText(text, languagecode, options.DefaultLanguageCode);
            return response.TranslatedText;
        }

        public IEnumerable<string> Translate(IEnumerable<string> texts, string languagecode, bool isHtml = false)
        {
            if (!IsEnabled()) return default;

            IList<TranslationResult> response;

            if (isHtml) response = client.TranslateHtml(texts, languagecode, options.DefaultLanguageCode);
            else response = client.TranslateText(texts, languagecode, options.DefaultLanguageCode);
            return response.Select(t => t.TranslatedText);
        }


        public Dictionary<string, string> Translate(string text, bool isHtml = false)
        {
            if (!IsEnabled()) return default;

            Dictionary<string, string> result = new();
            foreach (var code in options.LanguageCodes)
                result.Add(code,Translate(text,code, isHtml));
            return result;
        }
      
        public Dictionary<string, IEnumerable<string>> Translate(IEnumerable<string> texts, bool isHtml = false)
        {
            if (!IsEnabled()) return default;

            Dictionary<string, IEnumerable<string>> result = new();
            foreach (var code in options.LanguageCodes)
                result.Add(code, Translate(texts, code, isHtml));
            return result;
        }


        public  Dictionary<string, List<string>> Translate(IEnumerable<(string value, ITranslateAttribute info)> texts)
        {
            if (!IsEnabled()) return default;

            Dictionary<string, List<string>> result = new();

            var isHtmlGroups = texts.GroupBy(t => t.info.GetIsHtml).OrderBy(g => g.Key);

            foreach (var group in isHtmlGroups)
            {
                var values = group.Select(g => g.value);
                var ishtml = group.First().info.GetIsHtml;
                foreach (var code in options.LanguageCodes)
                {
                    if (result.ContainsKey(code))
                    {
                        result[code].AddRange(Translate(values, code, ishtml));
                        continue;
                    }

                    result.Add(code, (Translate(values, code, ishtml)).ToList());
                }
            }

            return result;
        }




        public async ValueTask<string> TranslateAsync(string text, string languagecode, bool isHtml = false)
        {
            if (!IsEnabled()) return default;

            TranslationResult response;

            if (isHtml) response = await client.TranslateHtmlAsync(text, languagecode, options.DefaultLanguageCode);
            else response = await client.TranslateTextAsync(text, languagecode, options.DefaultLanguageCode);
            return response.TranslatedText;
        }

        public async ValueTask<IEnumerable<string>> TranslateAsync(IEnumerable<string> texts, string languagecode, bool isHtml = false)
        {
            if (!IsEnabled()) return default;

            IList<TranslationResult> response;

            if (isHtml) response = await client.TranslateHtmlAsync(texts, languagecode, options.DefaultLanguageCode);
            else response = await client.TranslateTextAsync(texts, languagecode, options.DefaultLanguageCode);
            return response.Select(t=>t.TranslatedText);
        }


        public async ValueTask<Dictionary<string, string>> TranslateAsync(string text, bool isHtml = false)
        {
            if (!IsEnabled()) return default;

            Dictionary<string, string> result = new();
            foreach (var code in options.LanguageCodes)
                result.Add(code, await TranslateAsync(text, code, isHtml) );
            return result;
        }

        public async ValueTask<Dictionary<string, IEnumerable<string>>> TranslateAsync(IEnumerable<string> texts, bool isHtml = false)
        {
            if (!IsEnabled()) return default;

            Dictionary<string, IEnumerable<string>> result = new();
            foreach (var code in options.LanguageCodes)
                result.Add(code,await TranslateAsync(texts, code, isHtml));
            return result;
        }


        public async ValueTask<Dictionary<string, List<string>>> TranslateAsync(IEnumerable<(string value, ITranslateAttribute info)> texts)
        {
            if (!IsEnabled()) return default;

            Dictionary<string, List<string>> result = new();

            var isHtmlGroups = texts.GroupBy(t => t.info.GetIsHtml).OrderBy(g=>g.Key);

            foreach (var group in isHtmlGroups)
            {
                var values = group.Select(g => g.value);
                var ishtml = group.First().info.GetIsHtml;
                foreach (var code in options.LanguageCodes)
                {
                    if (result.ContainsKey(code))
                    {
                        result[code].AddRange(await TranslateAsync(values, code, ishtml));
                        continue;
                    }

                    result.Add(code, (await TranslateAsync(values, code, ishtml)).ToList());
                }
            }

            return result;
        }

    }
}

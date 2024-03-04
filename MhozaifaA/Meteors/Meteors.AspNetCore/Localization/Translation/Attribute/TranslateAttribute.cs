using Meteors.AspNetCore.Core.Culture;
using Meteors.AspNetCore.Helper.ExtensionMethods.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Localization.Translation
{
    public interface ITranslateAttribute
    {
        string[] GetCodes { get; }
        bool GetIsHtml { get; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false,Inherited =false)]
    public class TranslateAttribute : Attribute , ITranslateAttribute
    {
        private readonly string[] codes;
        private readonly bool isHtml;

        public TranslateAttribute() { } //default service
        public TranslateAttribute(bool isHtml) : this() { this.isHtml = isHtml; }
        public TranslateAttribute(bool isHtml= default, params string[] codes) : this(isHtml) { this.codes = codes; }
        public TranslateAttribute(params string[] codes) : this() { this.codes = codes; }
        public TranslateAttribute(bool isHtml = default, params LanguageCode[] codes) : this(isHtml,codes.ToStringArray()) { }
        public TranslateAttribute(params LanguageCode[] codes) : this(codes.ToStringArray()) { }

        public string[] GetCodes => this.codes;
        public bool GetIsHtml => this.isHtml;
    }
}

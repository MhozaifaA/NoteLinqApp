using Meteors.AspNetCore.Core.Culture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Core.DataStructures
{
    public class PropertyTranslate : Dictionary<string, string> {

        public PropertyTranslate() { }
        public PropertyTranslate(Dictionary<string, string> dic)
        {
            foreach (var item in dic)
                Add(item.Key,item.Value);
        }
    
    }

    public class CultureDictionary : Dictionary<string, PropertyTranslate>
    {

        public PropertyTranslate this[int index]
        {
            get => base[base.Keys.ElementAt(index)];
        }

        public PropertyTranslate this[LanguageCode key]
        {
            get => base[key.ToString()];
        }


        public void AddOrUpdate(string code, PropertyTranslate translate)
        {
            if (TryUpdate(code, translate)) return;
            base.Add(code, translate);
        }

        public void AddOrUpdate(LanguageCode code, PropertyTranslate translate)
        {
            AddOrUpdate(code.ToString(), translate);
        }

        public bool TryAddOrUpdate(string code, PropertyTranslate translate)
        {
            if (TryUpdate(code, translate)) return true;
            if (base.TryAdd(code, translate)) return true;
            return false;
        }
        public bool TryAddOrUpdate(LanguageCode code, PropertyTranslate translate)
        {
            return TryAddOrUpdate(code.ToString(), translate);
        }

        public void Update(string code, PropertyTranslate translate)
        {
            base[code] = translate;
        }

        public void Update(LanguageCode code, PropertyTranslate translate)
        {
            Update(code.ToString(),translate);
        }

        public bool TryUpdate(string code, PropertyTranslate translate)
        {
            if (base.ContainsKey(code))
            {
                base[code] = translate;
                return true;
            }
            return false;
        }

        public bool TryUpdate(LanguageCode code, PropertyTranslate translate)
        {
            return TryUpdate(code.ToString(),translate);
        }

        public void Add(LanguageCode code, PropertyTranslate translate)
        {
            base.Add(code.ToString(), translate);
        }

        public bool TryAdd(LanguageCode code, PropertyTranslate translate)
        {
           return base.TryAdd(code.ToString(), translate);
        }

        public void Remove(LanguageCode code)
        {
            base.Remove(code.ToString());
        }

    }
}
